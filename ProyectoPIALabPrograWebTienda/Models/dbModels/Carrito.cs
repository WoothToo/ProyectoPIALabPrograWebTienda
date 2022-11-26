using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    [Table("Carrito")]
    public partial class Carrito
    {
        [Key]
        public int Idusuario { get; set; }
        [Key]
        public int Idproducto { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("Idproducto")]
        [InverseProperty("Carritos")]
        public virtual Producto IdproductoNavigation { get; set; } = null!;
        [ForeignKey("Idusuario")]
        [InverseProperty("Carritos")]
        public virtual ApplicationUser IdusuarioNavigation { get; set; } = null!;
    }
}
