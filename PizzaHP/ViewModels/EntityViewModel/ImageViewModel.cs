using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHP.ViewModels
{
    public class ImageViewModel
    {
        public ImageViewModel(int ID, string path)
        {
            IngID = ID;
            Path = path;
        }

        public int IngID { get; set; }
        public string Path { get; set; }
    }
}
