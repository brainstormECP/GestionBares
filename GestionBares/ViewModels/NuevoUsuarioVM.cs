using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBares.ViewModels
{
    public class NuevoUsuarioVM
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y como maximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmacion no coinciden.")]
        public string ConfirmPassword { get; set; }

        public List<string> RolesIds { get; set; }

        public NuevoUsuarioVM()
        {
            RolesIds = new List<string>();
        }
    }
}
