using Microsoft.EntityFrameworkCore;
using WebAppSwager1.Models;

namespace WebAppSwager1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options){ 
        }

        //public DbSet<tUser> tUser { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
