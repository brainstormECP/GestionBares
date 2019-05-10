using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class SolicitudVM
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public ICollection<SolicitudBarVM> Bares { get; set; }
        public double EnAlmacen { get; set; }
        public SolicitudVM()
        {
            Bares = new List<SolicitudBarVM>();
        }
    }
}