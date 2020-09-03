using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
            _logger.LogInformation("Creating table with Name = {name}", name);

            try
            {
                var isDatabaseAvailable = _databaseService.CheckIfDatabaseAvailable();
                if (isDatabaseAvailable)
                {
                    _logger.LogDebug("Database is available");

                    var tableDate = new DateTime(2000, 12, 12, 12, 12, 12, DateTimeKind.Utc);
                    
                    var table = new Table
                    {
                        Name = name, 
                        Date = tableDate
                    };

                    _logger.LogDebug("Table object successfully initialized with name={name} and date = {tableDate}", name, tableDate);

                    _logger.LogTrace("Before serializing table object");

                    var serializeObject = JsonConvert.SerializeObject(table, new JsonSerializerSettings {Converters = { new JavaScriptDateTimeConverter() } });

                    _logger.LogTrace("after serializing table object");

                    _tableManagementService.Create(serializeObject);

                    _exceptionService.ThrowException();
                    _logger.LogInformation("Table {name} successfully created in Database", name);
                }
                else
                {
                    _logger.LogWarning("Database is not available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Table {name} failed to create", name);
            }

            return View("Index");
        }
    }
}