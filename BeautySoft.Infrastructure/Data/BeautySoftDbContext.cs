using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySoft.Infrastructure.Data
{
    public class BeautySoftDbContext : DbContext
    {
        public BeautySoftDbContext(DbContextOptions<BeautySoftDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Estilista> Estilistas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Comision> Comisiones { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<HistorialServicio> HistorialServicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CLIENTE
            modelBuilder.Entity<Cliente>(e =>
            {
                e.ToTable("Clientes");
                e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                e.Property(p => p.Telefono).HasMaxLength(20);
                e.Property(p => p.Email).HasMaxLength(100);
                e.HasIndex(p => p.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            });

            // ESTILISTA
            modelBuilder.Entity<Estilista>(e =>
            {
                e.ToTable("Estilistas");
                e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                e.Property(p => p.Telefono).HasMaxLength(20);
                e.Property(p => p.Email).HasMaxLength(100);
                e.Property(p => p.PorcentajeComision).HasColumnType("decimal(5,2)");
                e.HasIndex(p => p.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            });

            // SERVICIO
            modelBuilder.Entity<Servicio>(e =>
            {
                e.ToTable("Servicios");
                e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                e.HasIndex(p => p.Nombre).IsUnique();
                e.Property(p => p.Precio).HasColumnType("decimal(10,2)");
            });

            // CITA
            modelBuilder.Entity<Cita>(e =>
            {
                e.ToTable("Citas");
                e.Property(p => p.PrecioFinal).HasColumnType("decimal(10,2)");
                e.Property(p => p.Notas).HasMaxLength(255);

                // Enum mapeado como string (NO DbSet<EstadoCita>)
                e.Property(p => p.Estado)
                 .HasConversion<string>()
                 .HasMaxLength(20);

                e.HasOne(p => p.Cliente)
                 .WithMany(c => c.Citas)
                 .HasForeignKey(p => p.ClienteId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Estilista)
                 .WithMany(c => c.Citas)
                 .HasForeignKey(p => p.EstilistaId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Servicio)
                 .WithMany(c => c.Citas)
                 .HasForeignKey(p => p.ServicioId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // HISTORIAL SERVICIO
            modelBuilder.Entity<HistorialServicio>(e =>
            {
                e.ToTable("HistorialServicios");
                e.HasIndex(p => p.CitaId).IsUnique();
                e.Property(p => p.Observaciones).HasMaxLength(255);
                e.Property(p => p.ProductosUsados).HasMaxLength(255);

                e.HasOne(p => p.Cita)
                 .WithOne(c => c.HistorialServicio)
                 .HasForeignKey<HistorialServicio>(p => p.CitaId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // COMISION
            modelBuilder.Entity<Comision>(e =>
            {
                e.ToTable("Comisiones");
                e.Property(p => p.Monto).HasColumnType("decimal(10,2)");

                e.HasOne(p => p.Estilista)
                 .WithMany(ei => ei.Comisiones)
                 .HasForeignKey(p => p.EstilistaId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Cita)
                 .WithMany()
                 .HasForeignKey(p => p.CitaId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // PAGO
            modelBuilder.Entity<Pago>(e =>
            {
                e.ToTable("Pagos");
                e.Property(p => p.Monto).HasColumnType("decimal(10,2)");
                e.Property(p => p.MetodoPago).IsRequired().HasMaxLength(50);

                e.HasOne(p => p.Cita)
                 .WithMany(c => c.Pagos)
                 .HasForeignKey(p => p.CitaId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
