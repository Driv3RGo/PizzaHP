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
        private ShowBasket basket;      //Корзина
        public ObservableCollection<ImageViewModel> Images { get; set; }   //Картинка получаемой пиццы
        private List<IngredientViewModel> ing;      //Каталог ингредиентов
        private decimal sum;            //Сумма созданой пиццы
        private int massa;              //Масса созданой пиццы
        public List<int> IngredientID;  //Состав создаваемой пиццы
        private int sous_id = 0;        //Выбранный соус

        public BuildingPizza(ShowBasket sb)
        {
            db = new PizzaContext();
            basket = sb;
            Images = new ObservableCollection<ImageViewModel>();
            IngredientID = new List<int>();
            ing = db.Ingredient.ToList().Select(i => new IngredientViewModel(i)).ToList();
            AllIngredient = new List<IngredientViewModel>();
            selectedingredient = ing.Where(i => i.IngredientID == 1).SingleOrDefault();  
            sum = selectedingredient.Price;
            massa = selectedingredient.Massa;
            IngredientID.Add(selectedingredient.IngredientID);
        }

        private List<IngredientViewModel> _AllIngredient;       //Отображаемая категория ингредиентов
        public List<IngredientViewModel> AllIngredient
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

        private IngredientViewModel selectedingredient;         //Выбранный ингредиент
        public IngredientViewModel SelectIngredient
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
         
        private void Building()         //Построение пиццы
        {
            string path = "/PizzaHP;component/icons/Новая пицца/";
            Ingredient ing = new Ingredient();
            switch(selectedingredient.Kategori_FK)
            {
                case 2:             //Соус может быть только 1
                    {
                        if (sous_id == 0)
                        {
                            Sum += selectedingredient.Price;        //Прибавляем цену выбранного ингредиента
                            Massa += selectedingredient.Massa;      //Прибавляем массу выбранного ингредиента
                            IngredientID.Add(selectedingredient.IngredientID);  //Добавляем IngredientID в состав пиццы
                            sous_id = selectedingredient.IngredientID;      //Меняем id соуса
                            path += selectedingredient.Name + ".PNG";       //Задаем путь картинки
                            Images.Add(new ImageViewModel(selectedingredient.IngredientID, path));  //Добавляем картинку
                            selectedingredient.Color = "#FFFC0303";
                        }
                        else
                        {
                            ing = db.Ingredient.Find(sous_id);
                            Sum -= ing.Price;
                            Massa -= ing.Massa;
                            IngredientID.Remove(ing.IngredientID);
                            Images.Remove(Images.Where(i => i.IngID == ing.IngredientID).FirstOrDefault());
                            AllIngredient.Where(i => i.IngredientID == sous_id).FirstOrDefault().Color = null;

                            Sum += selectedingredient.Price;
                            Massa += selectedingredient.Massa;
                            IngredientID.Add(selectedingredient.IngredientID);
                            sous_id = selectedingredient.IngredientID;
                            path += selectedingredient.Name + ".PNG";
                            Images.Add(new ImageViewModel(selectedingredient.IngredientID, path));
                            selectedingredient.Color = "#FFFC0303";
                        }                      
                    }
                    break;
                default:
                    {
                        if (!IngredientID.Contains(selectedingredient.IngredientID))    //Повторный выбор уже добавленного ингридиента
                        {
                            Sum += selectedingredient.Price;
                            Massa += selectedingredient.Massa;
                            IngredientID.Add(selectedingredient.IngredientID);
                            if (selectedingredient.Kategori_FK == 3)
                                path += "Сыр" + ".PNG";
                            else path += selectedingredient.Name + ".PNG";
                            Images.Add(new ImageViewModel(selectedingredient.IngredientID, path));
                            selectedingredient.Color = "#FFFC0303";
                        }
                        else
                        {
                            ing = db.Ingredient.Find(selectedingredient.IngredientID);
                            Sum -= ing.Price;
                            Massa -= ing.Massa;
                            IngredientID.Remove(ing.IngredientID);
                            Images.Remove(Images.Where(i => i.IngID == ing.IngredientID).FirstOrDefault());
                            selectedingredient.Color = null;
                        }
                    }
                    break;
            }
            Images = Images;
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

        public RelayCommand FilterCommand       //Разделение ингредиентов по категориям
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    AllIngredient = ing.Where(i => i.Kategori_FK == Convert.ToInt32((string)obj)).ToList();
                }));
            }
        }

        public RelayCommand RestartCommand          //Начать создавать пиццу заново
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    Images.Clear();
                    IngredientID.Clear();
                    AllIngredient = new List<IngredientViewModel>();
                    ing = db.Ingredient.ToList().Select(i => new IngredientViewModel(i)).ToList();
                    selectedingredient = ing.Where(i => i.IngredientID == 1).SingleOrDefault();
                    Sum = selectedingredient.Price;
                    Massa = selectedingredient.Massa;
                    IngredientID.Add(selectedingredient.IngredientID);
                    sous_id = 0;
                }));
            }
        }    

        private RelayCommand addCommand;     //добавить в корзину
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new RelayCommand(obj =>
                { 
                    Product product = new Product
                    {
                        Name = "Конструктор",
                        Price = sum,
                        Size = 27,
                        MyPizza = true,
                        Visible = false,
                    };
                    db.Product.Add(product);

                    foreach (var pId in IngredientID)
                    {
                        Composition c = new Composition
                        {
                            Count = 1,
                            Ingredient_FK = pId,
                            Product_FK = product.ProductID,
                        };
                        db.Composition.Add(c);
                    }
                    db.SaveChanges();
                    basket.Update(product.ProductID);       //Добавляем пиццу в корзину
                    DisplayName = "Пицца добавлена в корзину";
                },
                (obj) => IngredientID.Count > 3));
            }
        }
    }
}
