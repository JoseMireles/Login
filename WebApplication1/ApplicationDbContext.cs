using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones)
        {
        }

        //Modelo
        public DbSet<Productos> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API PRODUCTOS
            modelBuilder.Entity<Productos>().HasKey(c => c.Productos_Id);
            modelBuilder.Entity<Productos>().Property(c => c.NombreArticulo).IsRequired();
            modelBuilder.Entity<Productos>().Property(c => c.Color).IsRequired();
            modelBuilder.Entity<Productos>().Property(c => c.Descripcion).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
