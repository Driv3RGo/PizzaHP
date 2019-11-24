using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ShowPizza : Base
    {
        private PizzaContext db;
        private ProductViewModel selectedproduct;
        public List<ProductViewModel> AllProduct { get; set; }

        private double _gridOpacity = 0.0;
        public double GridOpacity
        {
            get
            {
                return _gridOpacity;
            }
            set
            {
                _gridOpacity = value;
                OnPropertyChanged("GridOpacity");
            }
        }

        public ShowPizza()
        {
            db = new PizzaContext();
            AllProduct = db.Product.ToList().Select(i => new ProductViewModel(i)).ToList();
        }

        public ProductViewModel SelectProduct
        {
            get
            {
                return selectedproduct;
            }
            set
            {
                selectedproduct = value;
                OnPropertyChanged("SelectProduct");
            }
        }
    }
}
