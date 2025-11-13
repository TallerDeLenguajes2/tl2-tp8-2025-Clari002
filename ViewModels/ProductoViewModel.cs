using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp8_2025_Clari002.ViewModels
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [Display(Name = "Descripcion")]
        [StringLength(250, ErrorMessage = "Maximo 250 caracteres")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public double Precio { get; set; }
        
    }
}