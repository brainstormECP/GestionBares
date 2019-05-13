using GestionBares.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.ViewModels
{
    public class VentasDependientesPorBar
    {
        public int DependienteId { get; set; }
        public string DependienteNombre { get; set; }
        public List<VentasPorBar> VentasPorBares { get; set; }
        [BindProperty]
        [NotMapped]
        public List<CheckedTurno> CheckedTurnos { get; set; }
        [BindProperty]
        [NotMapped]
        public List<CheckedDependientes> CheckedDependientes { get; set; }

        public VentasDependientesPorBar()
        {
            VentasPorBares = new List<VentasPorBar>();
        }
    }

    public class VentasPorBar
    {
        public int BarId { get; set; }
        public string BarNombre { get; set; }
        public double PromedioVentasPorTurno { get; set; }

    }

}
