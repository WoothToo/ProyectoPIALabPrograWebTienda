using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        [Key]
        [Column("IDCategoria")]
        public int Idcategoria { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;

        [InverseProperty("IdcategoriaNavigation")]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
