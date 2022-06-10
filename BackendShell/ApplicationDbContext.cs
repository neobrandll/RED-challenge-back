using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class ApplicationDbContext: DbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
    }
}
