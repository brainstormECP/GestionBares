using GestionBares.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionBares.Models
{
    public class DependienteBar
    {
        public int Id { get; set; }
        [Display(Name = "Dependiente")]
        public int DependienteId { get; set; }
        public virtual Dependiente Dependiente { get; set; }
        [Display(Name = "Bar")]
        public int BarId { get; set; }
        public virtual Bar Bar { get; set; }
        [BindProperty]
        [NotMapped]
        public List<int> SelectBars { get; set; }
        [BindProperty]
        [NotMapped]
        public List<CheckedTurno> CheckedTurnos { get; set; }

        public DependienteBar()
        {
            CheckedTurnos = new List<CheckedTurno>();
        }
    }
}