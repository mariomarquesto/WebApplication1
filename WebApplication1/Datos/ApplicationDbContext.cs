using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contacto> Contactos { get; set; }
    }
}
