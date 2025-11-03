using Microsoft.AspNetCore.SignalR;

namespace tl2_tp8_2025_Clari002.Models
{
    public class PresupuestosDetalle
    {
        public Productos Producto { get; set; } = new Productos();
        public int Cantidad { get; set; }
    }
}