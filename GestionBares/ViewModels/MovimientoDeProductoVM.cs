using System;

namespace GestionBares.ViewModels
{
    public class MovimientoDeProductoVM
    {
        public DateTime Fecha { get; set; }
        public string Bar { get; set; }
        public string TipoDeMovimiento { get; set; }
        public decimal Cantidad { get; set; }

    }
}