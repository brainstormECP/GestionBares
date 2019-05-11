using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class SolicitudVM
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public int BarId { get; set; }
        public string Bar { get; set; }
        public double Cantidad { get; set; }
        public double EnAlmacen { get; set; }
    }
}