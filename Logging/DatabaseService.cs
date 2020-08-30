using System;
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

                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed attempt to check if Database is available: {e}");

                _logger.LogDebug("Internet connection problems");
            }

            return false;
        }
    }
}
