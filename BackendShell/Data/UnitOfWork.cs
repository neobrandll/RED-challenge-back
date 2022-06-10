using API.Core.IConfiguration;
using API.Core.IRepositories;
using API.Core.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(ApplicationDbContext context,
                ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            Orders = new OrderRepository(_context, _logger);
                 }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void  Dispose()
        {
             _context.Dispose();
        }

        public void Dispose()
        {
             _context.Dispose();
        }

    }
}
