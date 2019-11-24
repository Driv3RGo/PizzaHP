using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class BuildingPizza : Base
    {
        private PizzaContext db;
        public ObservableCollection<Ingredient> AllIngredient { get; set; }

        public BuildingPizza()
        {
            db = new PizzaContext();
            AllIngredient = new ObservableCollection<Ingredient>(db.Ingredient);
        }

        public ObservableCollection<Ingredient> SelectIngredient
        {
            get
            {
                return AllIngredient;
            }
            set
            {
                AllIngredient = value;
                OnPropertyChanged("SelectIngredient");
            }
        }

        private RelayCommand sousCommand;
        public RelayCommand SousCommand
        {
            get
            {
                return sousCommand ??
                  (sousCommand = new RelayCommand(obj =>
                  {
                      AllIngredient = new ObservableCollection<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 2));
                  }));
            }
        }
    }
}
