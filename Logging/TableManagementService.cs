using System;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class TableManagementService
    {
        private readonly ILogger<TableManagementService> _logger;


        public TableManagementService(ILogger<TableManagementService> logger)
        {
            _logger = logger;
        }


        public bool Create(string name)
        {
            try
            {
                _logger.LogInformation("Table with name {0} successfully created ", name);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Table with name {0} didn't create", name);
            }

            return false;
        }
    }
}