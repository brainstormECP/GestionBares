using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        public string Nombre { get; set; }

        public ICollection<UsuarioViewModel> Usuarios { get; set; }

        public RoleViewModel()
        {
            Usuarios = new List<UsuarioViewModel>();
        }
    }
}
