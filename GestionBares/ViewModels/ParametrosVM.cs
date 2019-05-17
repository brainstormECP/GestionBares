using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GestionBares.Models;

namespace GestionBares.ViewModels
{
    public enum FormaDePeriodo
    {
        Dias, Semanas, Meses, Años
    }
    public class ParametrosVM
    {
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime FechaFin { get; set; }
        [Display(Name = "Bares")]
        public int? BarId { get; set; }
        public List<int> Bares { get; set; }
        [Display(Name = "Dependientes")]
        public int? DependienteId { get; set; }
        public List<int> Dependientes { get; set; }
        [Display(Name = "Productos")]
        public int? ProductoId { get; set; }
        [Display(Name = "Forma de Periodo")]
        public FormaDePeriodo FormaDePeriodo { get; set; }
        public ParametrosVM()
        {
            Bares = new List<int>();
            Dependientes = new List<int>();
        }
    }
}