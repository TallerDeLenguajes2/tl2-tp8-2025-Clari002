using System.ComponentModel.DataAnnotations;

namespace tl2_tp8_2025_Clari002.ViewModels
{
    public class PresupuestoViewModel
    {
        public int IdPresupuesto { get; set; }

        [Display(Name = "Nombre del destinatario")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreDestinatario { get; set; } = string.Empty;

        [Display(Name = "Fecha de creacion")]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        
        
    }
}