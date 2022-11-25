using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    public partial class ProyectoPIALabPrograWebTiendaContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public ProyectoPIALabPrograWebTiendaContext()
        {
        }

        public ProyectoPIALabPrograWebTiendaContext(DbContextOptions<ProyectoPIALabPrograWebTiendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; } = null!;
        public virtual DbSet<DetallesVentum> DetallesVenta { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PK__Categori__70E82E285CE274D3");
            });

            modelBuilder.Entity<DetallesVentum>(entity =>
            {
                entity.HasKey(e => new { e.Idventa, e.Idproducto });

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetallesVenta_Productos");

                entity.HasOne(d => d.IdventaNavigation)
                    .WithMany(p => p.DetallesVenta)
                    .HasForeignKey(d => d.Idventa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetallesVenta_Venta");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productos_Categoria");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venta_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
