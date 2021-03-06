﻿using GestionBares.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.Models
{
    public class StandardVenta
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        [BindProperty]
        [NotMapped]
        public List<CheckedProductos> CheckedProductos { get; set; }

        public StandardVenta()
        {
            CheckedProductos = new List<CheckedProductos>();
        }

    }
}
