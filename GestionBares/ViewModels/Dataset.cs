using System.Collections.Generic;

namespace GestionBares.ViewModels
{
    public class Dataset
    {
        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public List<double> Data { get; set; }
        public bool Fill { get; set; }
        public Dataset()
        {
            Data = new List<double>();
        }

    }
}