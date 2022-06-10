using API.Core.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core.Repositories
{
    public class GenericRepository    
    {

        protected ApplicationDbContext _context;
        protected readonly ILogger _logger;

        public GenericRepository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

       
       

           }
}
