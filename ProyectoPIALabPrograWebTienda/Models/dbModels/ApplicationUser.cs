using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPIALabPrograWebTienda.Models.dbModels
{
    
    public partial class ApplicationUser:IdentityUser<int>
    {
        public ApplicationUser()
        {
            Carritos = new HashSet<Carrito>();
            Venta = new HashSet<Ventum>();
        }

       
        public string Nombres { get; set; } = null!;
        [StringLength(50)]
        public string Apellidos { get; set; } = null!;
        [StringLength(50)]
        public string Direccion { get; set; } = null!;
        [StringLength(50)]
        public string Ciudad { get; set; } = null!;
        [StringLength(50)]
        public string Pais { get; set; } = null!;
        [StringLength(50)]
        public string CodigoPostal { get; set; } = null!;

        [InverseProperty("IdusuarioNavigation")]
        public virtual ICollection<Ventum> Venta { get; set; }
        [InverseProperty("IdusuarioNavigation")]
        public virtual ICollection<Carrito> Carritos { get; set; }
    }
}
