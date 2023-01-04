using Microsoft.EntityFrameworkCore;

namespace FullStackDev.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<transaction> Transactions { get; set; }
    }
}
