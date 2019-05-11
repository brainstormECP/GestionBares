using System;
using System.Collections.Generic;
using GestionBares.Models;

namespace GestionBares.ViewModels
{
    public enum FormaDePeriodo
    {
        Dias, Semanas, Meses, Años
    }
    public class ParametrosVM
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? BarId { get; set; }
        public List<int> Bares { get; set; }
        public int? DependienteId { get; set; }
        public List<int> Dependientes { get; set; }
        public int? ProductoId { get; set; }
        public FormaDePeriodo FormaDePeriodo { get; set; }
        public ParametrosVM()
        {
            Bares = new List<int>();
            Dependientes = new List<int>();
        }
    }
}