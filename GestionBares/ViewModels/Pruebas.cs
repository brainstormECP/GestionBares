using System;
using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class Pruebas
    {
        public int CantidadDeTurnos { get; set; }
        public List<int> Bares { get; set; }
        public List<int> Dependientes { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CantidadMinima { get; set; }
        public int CantidadMaxima { get; set; }
        public int VentaMinima { get; set; }
        public int VentaMaxima { get; set; }
    }
}