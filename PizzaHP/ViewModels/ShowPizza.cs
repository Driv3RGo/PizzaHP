using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ShowPizza : ViewModelBase, IObservable
    {
        private PizzaContext db;
        private List<IObserver> observers;
        private ProductViewModel selectedproduct;       //Выбранный продукт
        public List<ProductViewModel> AllProduct { get; set; }  //Список продуктов
        public int ProductID;           //Элементы в корзине

        private double _gridOpacity = 0.0;      //Прозрачность grid, который содержит информации о продукте
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
            observers = new List<IObserver>();
            AllProduct = db.Product.ToList().Select(i => new ProductViewModel(i)).Where(i => !i.MyPizza).ToList();
        }

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.Update(ProductID);
            }
        }

        public ProductViewModel SelectProduct
        {
            get
            {
                return selectedproduct;
            }
            set
            {
                if(_gridOpacity == 0.0)
                    GridOpacity = 1.0;
                selectedproduct = value;           
                OnPropertyChanged("SelectProduct");
            }
        }

        public RelayCommand AddKorzina
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    ProductID = selectedproduct.ProductID;
                    NotifyObservers();
                }));
            }
        }
    }
}
