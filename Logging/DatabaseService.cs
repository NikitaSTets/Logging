using System;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class DatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly TableManagementService _tableManagementService;


        public DatabaseService(ILogger<DatabaseService> logger, TableManagementService tableManagementService)
        {
            _logger = logger;
            _tableManagementService = tableManagementService;
        }


        public bool CheckIfDatabaseAvailable()
        {
            try
            {
                var rand = new Random();
                var randomValue = rand.Next(1, 100);

                if (randomValue > 50)
                {
                    throw new UnauthorizedAccessException();
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Failed attempt to check if Database is available", e);
            }

            return false;
        }
    }
}
