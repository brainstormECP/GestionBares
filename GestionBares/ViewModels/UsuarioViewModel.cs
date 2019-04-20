using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.ViewModels
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }

        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public ICollection<RoleViewModel> Roles { get; set; }
        public List<string> RolesIds { get; set; }

        public UsuarioViewModel()
        {
            Roles = new List<RoleViewModel>();
            RolesIds = new List<string>();
        }
    }
}
