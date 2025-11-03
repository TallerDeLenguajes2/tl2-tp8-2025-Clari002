namespace tl2_tp8_2025_Clari002.Models
{
    public class Presupuestos
    {
        public int IdPresupuesto { get; set; }
        public string? NombreDestinatario { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public List<PresupuestosDetalle> Detalle { get; set; } = new List<PresupuestosDetalle>();

        public int CantidadProductos()
        {
            var suma = Detalle.Sum(d => d.Cantidad);
            return suma;
        }
        public double MontoPresupuesto()
        {
           return Detalle.Sum(d => d.Producto.Precio * d.Cantidad);
        }
            

        public double MontoPresupuestoConIva()
        {
            return MontoPresupuesto() * 1.21;
        }
          
    }
}