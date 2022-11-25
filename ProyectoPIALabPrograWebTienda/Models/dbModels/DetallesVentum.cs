using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    public partial class DetallesVentum
    {
        [Key]
        [Column("IDVenta")]
        public int Idventa { get; set; }
        [Key]
        [Column("IDProducto")]
        public int Idproducto { get; set; }
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("Idproducto")]
        [InverseProperty("DetallesVenta")]
        public virtual Producto IdproductoNavigation { get; set; } = null!;
        [ForeignKey("Idventa")]
        [InverseProperty("DetallesVenta")]
        public virtual Ventum IdventaNavigation { get; set; } = null!;
    }
}
