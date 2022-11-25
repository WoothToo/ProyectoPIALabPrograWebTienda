using System.ComponentModel.DataAnnotations;

namespace ProyectoPIALabPrograWebTienda.Models.DTO
{
    public class ProductoUpdateDTO
    {
        public int Idproducto { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        public int Idcategoria { get; set; }

        public decimal PrecioUnitario { get; set; }
        public IFormFile? ImagenArchivo { get; set; }
        public string? Imagen { get; set; }
    }
}
