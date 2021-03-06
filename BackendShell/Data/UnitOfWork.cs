using API.Core.IConfiguration;
using API.Core.IRepositories;
using API.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        private readonly SignInManager<IdentityUser> _signInManager;

        public UserManager<IdentityUser> UserManager { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IAuthRepository Auth { get; private set; }

        
        public UnitOfWork(ApplicationDbContext context,
                ILoggerFactory loggerFactory,
                UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager
                )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            _signInManager = signInManager;
            UserManager = userManager;

            Orders = new OrderRepository(_context, _logger);
            Auth = new AuthRepository(_context, _logger, userManager, _signInManager);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

       
        public void Dispose()
        {
             _context.Dispose();
        }

    }
}
