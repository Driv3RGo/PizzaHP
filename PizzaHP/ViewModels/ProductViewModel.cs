using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ProductViewModel : Base
    {
        private Product product;

        public ProductViewModel(Product pr)
        {
            product = pr;
        }

        public int ProductID
        {
            get { return product.ProductID; }
            set
            {
                product.ProductID = value;
                OnPropertyChanged("ProductID");
            }
        }

        public string Name
        {
            get { return product.Name; }
            set
            {
                product.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal Price
        {
            get { return product.Price; }
            set
            {
                product.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public int Size
        {
            get { return product.Size; }
            set
            {
                product.ProductID = value;
                OnPropertyChanged("Size");
            }
        }

        public bool MyPizza
        {
            get { return product.MyPizza; }
            set
            {
                product.MyPizza = value;
                OnPropertyChanged("MyPizza");
            }
        }

        public bool Visible
        {
            get { return product.Visible; }
            set
            {
                product.Visible = value;
                OnPropertyChanged("Visible");
            }
        }

        public string Picture
        {
            get { return product.Picture; }
            set
            {
                product.Picture = value;
                OnPropertyChanged("Picture");
            }
        }

        public string Ing
        {
            get { return String.Join(", ", product.Composition.Select(i => i.Ingredient.Name).ToList()); }
        }
    }
}
