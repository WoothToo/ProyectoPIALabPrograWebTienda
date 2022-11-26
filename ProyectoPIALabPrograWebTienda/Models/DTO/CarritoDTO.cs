using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPIALabPrograWebTienda.Models.DTO
{
    public class CarritoDTO
    {
        public int Idproducto { get; set; }
        public int Nombreproducto { get; set; }
        public int Precioproducto  { get; set; }
        public int Imagenproducto { get; set; }
        public int Cantidadproducto { get; set; }
    }
}
