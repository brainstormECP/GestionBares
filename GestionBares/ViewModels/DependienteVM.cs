using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class DependienteVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreDeUsuario { get; set; }
        public string UsuarioId { get; set; }
        public List<string> Bares { get; set; }
        public DependienteVM()
        {
            Bares = new List<string>();
        }
    }
}