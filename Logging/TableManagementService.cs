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
            return string.IsNullOrEmpty(name);
        }
    }
}