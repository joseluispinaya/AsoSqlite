using AsoSqlite.Mobile.Modelos;
using AsoSqlite.Mobile.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace AsoSqlite.Mobile.DataAccess
{
    public class EAfiliadoDbContext : DbContext
    {
        public DbSet<EAfiliado> EAfiliados { get; set; }
        public DbSet<EAsociacion> EAsociaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("afiliados.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EAfiliado>(entity =>
            {
                entity.HasKey(col => col.IdAfiliado);
                entity.Property(col => col.IdAfiliado).IsRequired().ValueGeneratedOnAdd();

                entity.HasOne(a => a.EAsociacion)
                .WithMany(g => g.Afiliados)
                .HasForeignKey(a => a.EAsociacionId);
            });

            modelBuilder.Entity<EAsociacion>(entity =>
            {
                entity.HasKey(col => col.Idasoci);
                entity.Property(col => col.Idasoci).IsRequired(); // No autogenerado
            });
        }
    }
}
