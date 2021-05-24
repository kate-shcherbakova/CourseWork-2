using Microsoft.EntityFrameworkCore;
using MvcAvia.Models;

namespace MvcAvia.Data
{
    // контекст бази даних
    public class MvcAviaContext : DbContext
    {
        public MvcAviaContext(DbContextOptions<MvcAviaContext> options)
            : base(options)
        {
        }

        // набір сутностей - таблиця Airport у базі даних
        public DbSet<Airport> Airport { get; set; }
        // набір сутностей - таблиця Search у базі даних
        public DbSet<Search> Search { get; set; }
    }
}
