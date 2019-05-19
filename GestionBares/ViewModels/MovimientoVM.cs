namespace GestionBares.ViewModels
{
    public class MovimientoVM
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public double Inicio { get; set; }
        public double Entradas { get; set; }
        public double Enviados { get; set; }
        public double Recibidos { get; set; }
        public double Fin { get; set; }
        public double Consumo { get { return Inicio + Entradas - Enviados + Recibidos - Fin < 0 ? 0 : Inicio + Entradas - Enviados + Recibidos - Fin; } }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
    }
}