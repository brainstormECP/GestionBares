using System;
using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class ControlExistenciaVM
    {
        public int Id { get; set; }
        public int TurnoId { get; set; }
        public string Bar { get; set; }
        public string Dependiente { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleExistenciaVM> Detalles { get; set; }
        public ControlExistenciaVM()
        {
            Detalles = new List<DetalleExistenciaVM>();
        }
    }
}