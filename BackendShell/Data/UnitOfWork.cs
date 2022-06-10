using API.Core.IConfiguration;
using API.Core.IRepositories;
using Microsoft.Extensions.Logging;
using System;

namespace API.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public IOrderRepository Orders { get; private set; }

        public UnitOfWork()
        {
            ApplicationDbContext context,
                ILoggerFactory loggerFactory        }
    }
}
