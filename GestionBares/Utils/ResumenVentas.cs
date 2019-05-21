using System;

namespace GestionBares.Utils
{
    public class ResumenVentas
    {
        public int TurnoId { get; set; }
        public DateTime Fecha { get; set; }
        public double Ventas { get; set; }
        public double Costos { get; set; }
    }
}