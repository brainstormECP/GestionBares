using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.ViewModels
{
    public class SeleccionarAsignacionVM
    {
        [BindProperty]
        public List<CheckedDependientes> CheckedDependientes { get; set; }
        public List<CheckedTurno> CheckedTurnos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

        public SeleccionarAsignacionVM()
        {
            CheckedDependientes = new List<CheckedDependientes>();
            CheckedTurnos = new List<CheckedTurno>();
            FechaInicio = new DateTime(2012, 10, 31);
            FechaFinal = DateTime.Now;
        }
    }
}
