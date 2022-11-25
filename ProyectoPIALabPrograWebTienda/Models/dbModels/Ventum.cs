using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetallesVenta = new HashSet<DetallesVentum>();
        }

        [Key]
        [Column("IDVenta")]
        public int Idventa { get; set; }
        [Column("IDUsuario")]
        public int Idusuario { get; set; }
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
        [Column(TypeName = "money")]
        public decimal PrecioFinal { get; set; }

        [ForeignKey("Idusuario")]
        [InverseProperty("Venta")]
        public virtual ApplicationUser IdusuarioNavigation { get; set; } = null!;
        [InverseProperty("IdventaNavigation")]
        public virtual ICollection<DetallesVentum> DetallesVenta { get; set; }
    }
}
