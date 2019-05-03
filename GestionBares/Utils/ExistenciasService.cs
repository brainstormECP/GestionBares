using System.Collections.Generic;
using System.Linq;
using GestionBares.Data;
using GestionBares.Models;

namespace GestionBares.Utils
{
    public class ExistenciasService
    {
        private readonly ApplicationDbContext _db;

        public ExistenciasService(ApplicationDbContext context)
        {
            _db = context;
        }

        public List<DetallePedidoAlmacen> ExistenciaDeBar(int barId)
        {
            var turno = _db.Set<Turno>().Where(t => t.BarId == barId).OrderBy(t => t.FechaInicio).Last();
            return ExistenciaDeBarPorTurno(turno.Id);
        }

        public List<DetallePedidoAlmacen> ExistenciaDeBarPorTurno(int turnoId)
        {
            var turno = _db.Set<Turno>().FirstOrDefault(t => t.Id == turnoId);
            var result = new List<DetallePedidoAlmacen>();

            return result;
        }

    }
}