using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Formatting.Compact;

namespace Logging
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExceptionService _exceptionService;
        private readonly DatabaseService _databaseService;
        private readonly TableManagementService _tableManagementService;


        public HomeController(ILogger<HomeController> logger, ExceptionService exceptionService, DatabaseService? databaseService, TableManagementService tableManagementService)
        {
            _logger = logger;
            _exceptionService = exceptionService;
            _databaseService = databaseService;
            _tableManagementService = tableManagementService;
        }


        public IActionResult Index()
        {
            _logger.LogInformation("User navigated to HomeController index page");

            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            _logger.LogTrace("HomeController Line 34");
            try
            {
                var isDatabaseAvailable = _databaseService.CheckIfDatabaseAvailable();
                _logger.LogTrace("HomeController Line 38");
                if (isDatabaseAvailable)
                {
                    _logger.LogTrace("HomeController Line 41");
                    _tableManagementService.Create(name);

                    _exceptionService.ThrowException();
                    _logger.LogInformation("Table with name {0} successfully created ", name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogTrace("HomeController Line 49");
                _logger.LogError(ex, $"Table with name {name} didn't create");
            }

            return View("Index");
        }
    }
}