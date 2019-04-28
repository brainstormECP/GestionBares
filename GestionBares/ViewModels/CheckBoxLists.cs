using GestionBares.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.ViewModel
{
    public class CheckedProductos
    {
        public int ProductoId { get; set; }        
        public bool Checked { get; set; }
        public Producto Producto { get; set; }        
    }

    public class CheckedTurno
    {
        public int BarId { get; set; }
        public bool Checked { get; set; }
        public Bar Bar { get; set; }
    }
}
