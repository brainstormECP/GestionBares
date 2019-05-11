using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class DatosGraficas
    {
        public List<string> Labels { get; set; }
        public List<Dataset> Datasets { get; set; }
        public DatosGraficas()
        {
            Labels = new List<string>();
            Datasets = new List<Dataset>();
        }
    }
}