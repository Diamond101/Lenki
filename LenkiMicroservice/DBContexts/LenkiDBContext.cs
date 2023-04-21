using LenkiMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace LenkiMicroservice.DBContexts
{
    public class LenkiDBContext : DbContext
    {
        public LenkiDBContext(DbContextOptions<LenkiDBContext> options) : base(options)
        {

        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Users> Customers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ReservedBooks> ReservedBooks { get; set; }
        public DbSet<BorrowBooks> BorrowBooks { get; set; }
    }
}
