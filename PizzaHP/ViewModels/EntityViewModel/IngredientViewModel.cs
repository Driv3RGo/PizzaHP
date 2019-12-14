using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        private Ingredient ing;

        public IngredientViewModel(Ingredient ingredient)
        {
            ing = ingredient;
        }

        public int IngredientID
        {
            get { return ing.IngredientID; }
            set
            {
                ing.IngredientID = value;
                OnPropertyChanged("IngredientID");
            }
        }

        public string Name
        {
            get { return ing.Name; }
            set
            {
                ing.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal Price
        {
            get { return ing.Price; }
            set
            {
                ing.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public int Massa
        {
            get { return ing.Massa; }
            set
            {
                ing.Massa = value;
                OnPropertyChanged("Massa");
            }
        }

        public int Kategori_FK
        {
            get { return ing.Kategori_FK; }
            set
            {
                ing.Kategori_FK = value;
                OnPropertyChanged("Kategori_FK");
            }
        }

        public string Picture
        {
            get { return ing.Picture; }
            set
            {
                ing.Picture = value;
                OnPropertyChanged("Picture");
            }
        }

        private string color = null;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }
    }
}
