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
    public class BuildingPizza : ViewModelBase
    {
        private PizzaContext db;
        public ObservableCollection<ImageViewModel> Images { get; set; }   
        private decimal sum;        //Сумма созданой пиццы
        private int massa;       //Масса созданой пиццы
        public List<int> IngredientID;   //Состав создаваемой пиццы
        private int sous_id = 0;        //Выбранный соус

        public BuildingPizza()
        {
            db = new PizzaContext();
            selectedingredient = db.Ingredient.Where(i => i.IngredientID == 1).SingleOrDefault();
            Images = new ObservableCollection<ImageViewModel>();
            sum = selectedingredient.Price;
            massa = selectedingredient.Massa;
            IngredientID = new List<int>();
            Filtration();
        }

        public List<Ingredient> Sous { get; set; }
        public List<Ingredient> Cheese { get; set; }
        public List<Ingredient> Meat { get; set; }
        public List<Ingredient> Fish { get; set; }
        public List<Ingredient> Grib { get; set; }
        public List<Ingredient> Tomato { get; set; }
        public List<Ingredient> Salt { get; set; }
        private void Filtration()       //Фильтр ингредиентов
        {
            Sous = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 2));
            Cheese = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 3));
            Meat = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 4));
            Fish = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 5));
            Grib = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 6));
            Tomato = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 7));
            Salt = new List<Ingredient>(db.Ingredient.Where(i => i.Kategori_FK == 8));
        }

        private List<Ingredient> _AllIngredient;
        public List<Ingredient> AllIngredient
        {
            get
            {
                return _AllIngredient;
            }
            set
            {
                _AllIngredient = value;
                OnPropertyChanged("AllIngredient");
            }
        }

        private Ingredient selectedingredient;
        public Ingredient SelectIngredient
        {
            get
            {
                return selectedingredient;
            }
            set
            {
                selectedingredient = value;
                Building();
                OnPropertyChanged("SelectIngredient");
            }
        }      
           
        private void Building()
        {
            string path = "/PizzaHP;component/icons/Новая пицца/";
            Ingredient ing = new Ingredient();
            switch(selectedingredient.Kategori_FK)
            {
                case 2:             //Соус может быть только 1
                    {
                        if (sous_id == 0)
                        {
                            Sum += selectedingredient.Price;
                            Massa += selectedingredient.Massa;
                            IngredientID.Add(selectedingredient.IngredientID);
                            sous_id = selectedingredient.IngredientID;
                            path += selectedingredient.Name + ".PNG";
                            Images.Add(new ImageViewModel(selectedingredient.IngredientID, path));
                        }
                        else
                        {
                            ing = db.Ingredient.Find(sous_id);
                            Sum -= ing.Price;
                            Massa -= ing.Massa;
                            IngredientID.Remove(ing.IngredientID);
                            Images.Remove(Images.Where(i => i.IngID == ing.IngredientID).FirstOrDefault());

                            Sum += selectedingredient.Price;
                            Massa += selectedingredient.Massa;
                            IngredientID.Add(selectedingredient.IngredientID);
                            sous_id = selectedingredient.IngredientID;
                            path += selectedingredient.Name + ".PNG";
                            Images.Add(new ImageViewModel(selectedingredient.IngredientID, path));
                        }
                        Images = Images;
                    }
                    break;
                default:
                    {
                        if (!IngredientID.Contains(selectedingredient.IngredientID))
                        {
                            Sum += selectedingredient.Price;
                            Massa += selectedingredient.Massa;
                            IngredientID.Add(selectedingredient.IngredientID);
                            if (selectedingredient.Kategori_FK == 3)
                                path += "Сыр" + ".PNG";
                            else path += selectedingredient.Name + ".PNG";
                            Images.Add(new ImageViewModel(SelectIngredient.IngredientID, path));                        
                        }
                        else
                        {
                            ing = db.Ingredient.Find(selectedingredient.IngredientID);
                            Sum -= ing.Price;
                            Massa -= ing.Massa;
                            IngredientID.Remove(ing.IngredientID);
                            Images.Remove(Images.Where(i => i.IngID == ing.IngredientID).FirstOrDefault());
                        }
                        Images = Images;
                    }
                    break;
            }
        }

        public decimal Sum
        {
            get { return sum; }
            set { sum = value; OnPropertyChanged("Sum"); }
        }

        public int Massa
        {
            get { return massa; }
            set { massa = value; OnPropertyChanged("Massa"); }
        }

        public RelayCommand FilterCommand
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    AllIngredient = (List<Ingredient>)obj;
                }));
            }
        }
    }
}
