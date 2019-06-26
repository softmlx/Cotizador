using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cotizador.UI.Models
{
    public class BDConnectModel : DbContext
    {
        public BDConnectModel() : base("Cotizador.UI.Properties.Settings.ConStringLocal")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Categoria)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Marca>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Marca)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Moneda>()
                .HasMany(e => e.Cotizacion)
                .WithRequired(e => e.Moneda)
                .WillCascadeOnDelete(false);

            /*modelBuilder.Entity<Moneda>()
                .HasMany(e => e.InformacionComercialProductos)
                .WithRequired(e => e.Monedas)
                .WillCascadeOnDelete(false);
                
            modelBuilder.Entity<Moneda>()
                .HasMany(e => e.TipoCambioOrigen)
                .WithRequired(e => e.MonedaOrigen)
                .HasForeignKey(e => e.Moneda1Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Moneda>()
                .HasMany(e => e.TipoCambioDestino)
                .WithRequired(e => e.MonedasDestino)
                .HasForeignKey(e => e.Moneda2Id)
                .WillCascadeOnDelete(false);

            */
            modelBuilder.Entity<Estado>()
                .HasMany(e => e.UbicacionGeografica)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Municipio>()
                .HasMany(e => e.UbicacionGeografica)
                .WithRequired(e => e.Municipio)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Pai>()
                .HasMany(e => e.UbicacionGeografica)
                .WithRequired(e => e.Pais)
                .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Product>()
            //    .HasOptional(i => i.InfoComercial)
            //    .WithRequired(p => p.Producto)
            //    .WillCascadeOnDelete(false);
            modelBuilder.Entity<InformacionComercialProducto>()
                .HasRequired(e => e.Producto)
                .WithOptional(p => p.InfoComercial)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<DetalleCotizacion>()
                .HasRequired(p => p.Producto);
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<BitPermission> BitPermission { get; set; }
        public DbSet<BitRole> BitRole { get; set; }
        public DbSet<BitUser> BitUser { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ContactoCliente> ContactoCliente { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<BitMenuItem> BitMenuItem { get; set; }
        public DbSet<BitUserClaim> BitUserClaim { get; set; }
        public DbSet<BitUserLogin> BitUserLogin { get; set; }
        public DbSet<BitUserRole> BitUserRole { get; set; }
        public DbSet<BitRolePermission> BitRolePermission { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Pai> Pai { get; set; }
        //public DbSet<ContactoProveedor> ContactoProveedor { get; set; }
        public DbSet<UnidadMedida> UnidadMedida { get; set; }
        public DbSet<Cotizacion> Cotizacion { get; set; }
        public DbSet<UbicacionGeografica> UbicacionGeografica { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<TipoCambio> TipoCambio { get; set; }
        public DbSet<InformacionFiscalProveedor> InformacionFiscalProveedores { get; set; }
        public DbSet<InformacionFiscal> InformacionFiscal { get; set; }
        public DbSet<ConfiguracionSistema> ConfiguracionSistema { get; set; }
        public DbSet<DetalleCotizacion> DetalleCotizacion { get; set; }
        public DbSet<InformacionComercialProducto> InformacionComercialProductos { get; set; }
    }
}