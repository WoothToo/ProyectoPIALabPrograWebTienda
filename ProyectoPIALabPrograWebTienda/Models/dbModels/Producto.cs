using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    public partial class Producto
    {
        public Producto()
        {
            Carritos = new HashSet<Carrito>();
            DetallesVenta = new HashSet<DetallesVentum>();
        }

        [Key]
        [Column("IDProducto")]
        public int Idproducto { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Column("IDCategoria")]
        public int Idcategoria { get; set; }
        [Column(TypeName = "money")]
        public decimal PrecioUnitario { get; set; }
        public string Imagen { get; set; } = null!;

        [ForeignKey("Idcategoria")]
        [InverseProperty("Productos")]
        public virtual Categorium IdcategoriaNavigation { get; set; } = null!;
        [InverseProperty("IdproductoNavigation")]
        public virtual ICollection<Carrito> Carritos { get; set; }
        [InverseProperty("IdproductoNavigation")]
        public virtual ICollection<DetallesVentum> DetallesVenta { get; set; }
    }
}
