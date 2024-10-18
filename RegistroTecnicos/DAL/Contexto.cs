using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;
namespace RegistroTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options)
        : base(options) { }
    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<TiposTecnicos> TiposTecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Trabajos> Trabajos { get; set; }
    public DbSet <TrabajosDetalle> TrabajosDetalles { get; set; }
    public DbSet<Prioridades> Prioridades { get; set; }
    public DbSet <Articulos> Articulos { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
    {
        new Articulos { ArticuloId = 1, Descripcion = "Lapicero", Costo = 5.00m, Precio = 10.00m, Existencia = 100 },
        new Articulos { ArticuloId = 2, Descripcion = "PaletaPayaso", Costo = 35.00m, Precio = 50.00m, Existencia = 50 },
        new Articulos { ArticuloId = 3, Descripcion = "Funda de pan", Costo = 45.00m, Precio = 60.00m, Existencia = 15 }
    });
        modelBuilder.Entity<Tecnicos>()
           .HasOne(tt => tt.TiposTecnicosId)
           .WithMany(t => t.Tecnicos)
           .HasForeignKey(t => t.TipoTecnicoId);

        base.OnModelCreating(modelBuilder);
    }
}