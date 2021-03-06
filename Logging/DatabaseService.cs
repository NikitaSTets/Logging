﻿using System;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class DatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly ExceptionService _exceptionService;


        public DatabaseService(ILogger<DatabaseService> logger, ExceptionService exceptionService)
        {
            _logger = logger;
            _exceptionService = exceptionService;
        }


        public bool CheckIfDatabaseAvailable()
        {
            try
            {
                _exceptionService.ThrowException();

                _logger.LogDebug("Attempt to check if Database is available successfully");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed attempt to check if Database is available:");
            }

            return false;
        }
    }
}
