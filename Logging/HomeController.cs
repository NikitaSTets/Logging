using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            try
            {
                var isDatabaseAvailable = _databaseService.CheckIfDatabaseAvailable();
                if (isDatabaseAvailable)
                {
                    _tableManagementService.Create(name);
                }
            }
            catch (Exception ex)
            {
                //strack trace didn't log
                _logger.LogError("Exception thrown when trying to convert customer viewmodel to model or getting data from the database with id: " + 1231312, ex);

                //strack trace logged
                _logger.LogError($"An error accured: {ex}.");

                //strack trace logged
                _logger.LogError(ex, $"An error accured");
            }

            return View("Index");
        }
    }
}