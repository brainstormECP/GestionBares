namespace GestionBares.ViewModels
{
    public class DetalleExistenciaVM
    {
        public int ControlId { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public string Unidad { get; set; }
        public double CantidadAnterior { get; set; }
        public double Cantidad { get; set; }
    }
}