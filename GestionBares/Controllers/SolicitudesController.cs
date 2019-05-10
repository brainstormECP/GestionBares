using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GestionBares.Utils;
using GestionBares.ViewModels;

namespace GestionBares.Controllers
{
    [Authorize]
    public class SolicitudesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolicitudesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Traslados
        public IActionResult Productos()
        {
            var pedidos = _context.Set<DetallePedidoAlmacen>()
                .Include(t => t.Pedido.Turno.Bar)
                .Include(t => t.Pedido.Turno.Dependiente)
                .Include(t => t.Producto)
                .Where(t => !t.Atendido)
                .GroupBy(t => t.Producto)
                .Select(t => new SolicitudVM
                {
                    Producto = t.Key.Nombre,
                    ProductoId = t.Key.Id,
                    EnAlmacen = 0,
                    Bares = t.GroupBy(b => b.Pedido.Turno.Bar).Select(c => new SolicitudBarVM
                    {
                        Bar = c.Key.Nombre,
                        BarId = c.Key.Id,
                        Cantidad = c.Sum(s => s.Cantidad)
                    }).ToList()
                })
                .ToList();
            ViewBag.Bares = _context.Set<Bar>().Select(b => b.Nombre);
            return View(pedidos);
        }

        public IActionResult Ventas()
        {
            var pedidos = _context.Set<DetallePedidoAlmacenVenta>()
                .Include(t => t.Pedido.Turno.Bar)
                .Include(t => t.Pedido.Turno.Dependiente)
                .Include(t => t.Producto)
                .Where(t => !t.Atendido)
                .GroupBy(t => t.Producto)
                .Select(t => new SolicitudVM
                {
                    Producto = t.Key.Nombre,
                    ProductoId = t.Key.Id,
                    EnAlmacen = 0,
                    Bares = t.GroupBy(b => b.Pedido.Turno.Bar).Select(c => new SolicitudBarVM
                    {
                        Bar = c.Key.Nombre,
                        BarId = c.Key.Id,
                        Cantidad = c.Sum(s => s.Cantidad)
                    }).ToList()
                })
                .ToList();
            ViewBag.Bares = _context.Set<Bar>().Select(b => b.Nombre);
            return View(pedidos);
        }

        public IActionResult Atender(int id)
        {
            var pedidos = _context.Set<DetallePedidoAlmacen>().Where(d => !d.Atendido && d.ProductoId == id);
            foreach (var pedido in pedidos)
            {
                pedido.Atendido = true;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Productos));
        }

        public IActionResult AtenderVentas(int id)
        {
            var pedidos = _context.Set<DetallePedidoAlmacenVenta>().Where(d => !d.Atendido && d.ProductoId == id);
            foreach (var pedido in pedidos)
            {
                pedido.Atendido = true;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Ventas));
        }
    }
}
