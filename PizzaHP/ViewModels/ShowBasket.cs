using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ShowBasket : ViewModelBase, IObserver
    {
        private PizzaContext db;
        IObservable showpizza;
        private List<int> ProductID;     //Список продуктов в корзине

        public ShowBasket(IObservable obs)
        {
            db = new PizzaContext();
            AllProduct = new ObservableCollection<Product>();
            ProductID = new List<int>();
            Sum = 0;
            showpizza = obs;
            showpizza.AddObserver(this);
        }

        public void Update(object ob)
        {
            int pID = (int)ob;
            ProductID.Add(pID);
            Product pr = db.Product.Find(pID);
            _AllProduct.Add(pr);
            Sum += pr.Price;
            AllProduct = AllProduct;
        }

        private double _gridOpacity1 = 0.0;      //Прозрачность grida, который содержит информацию о товарах в корзине
        public double GridOpacity1
        {
            get
            {
                return _gridOpacity1;
            }
            set
            {
                _gridOpacity1 = value;
                OnPropertyChanged("GridOpacity1");
            }
        }

        private double _gridOpacity2 = 1.0;      //Прозрачность grida, который содержит информацию о пустой корзине
        public double GridOpacity2
        {
            get
            {
                return _gridOpacity2;
            }
            set
            {
                _gridOpacity2 = value;
                OnPropertyChanged("GridOpacity2");
            }
        }

        private decimal sum;        //Сумма всех пицц в корзине
        public decimal Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }

        private Product selectedproduct;       //Выбранный продукт
        public Product SelectProduct
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

        private ObservableCollection<Product> _AllProduct;      //Продукты, которые лежат в корзине
        public ObservableCollection<Product> AllProduct
        {
            get
            {
                return _AllProduct;
            }
            set
            {
                _AllProduct = value;
                if (AllProduct.Count == 0)
                {
                    GridOpacity1 = 0.0;
                    GridOpacity2 = 1.0;
                }
                else
                {
                    GridOpacity1 = 1.0;
                    GridOpacity2 = 0.0;
                }
                OnPropertyChanged("AllProduct");
            }
        }

        private RelayCommand deleteCommand;     //удаление из корзины
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                (deleteCommand = new RelayCommand(obj =>
                {
                    Product pr = db.Product.Find(selectedproduct.ProductID);
                    _AllProduct.Remove(pr);
                    ProductID.Remove(pr.ProductID);
                    Sum -= pr.Price;
                    AllProduct = AllProduct;
                },
                (obj) => selectedproduct != null));
            }
        }

        private string fio = null;      //Имя клиента
        public string FIO
        {
            get
            {
                return fio;
            }
            set
            {
                fio = value;
                OnPropertyChanged("FIO");
            }
        }

        private string phoneNumber = null;      //Телефон клиента
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        private string email = null;        //Почти клиента
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string address = null;        //Адрес доставки
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private RelayCommand orderService;
        public RelayCommand OrderService
        {
            get
            {
                return orderService ??
                (orderService = new RelayCommand(obj =>
                {
                    Client cl = new Client
                    {
                        FIO = fio,
                        PhoneNumber = phoneNumber,
                        Email = email
                    };
                    db.Client.Add(cl);

                    Random rand = new Random();
                    Order order = new Order
                    {
                        Client_FK = cl.ClientID,
                        Address = address,
                        DataBegin = DateTime.Now,
                        DataEnd = DateTime.Now.AddHours(rand.Next(3)).AddMinutes(rand.Next(50)),
                        Status_FK = 1,
                        Cost = sum,
                    };
                    db.Order.Add(order);

                    foreach (var pId in ProductID)
                    {
                        Order_line ol = new Order_line
                        {
                            Order_FK = order.OrderID,
                            Product_FK = pId,
                            Count = 1,                   
                        };
                        db.Order_line.Add(ol);
                    }
                    //if(Save())
                    //{
                    //    Sum = 0;
                    //    FIO = null;
                    //    PhoneNumber = null;
                    //    Email = null;
                    //    Address = null;
                    //    AllProduct.Clear();
                    //    ProductID.Clear();
                    //}
                },
                (obj) => FIO != null && PhoneNumber != null && Email != null && Address != null));  //условие, при котором будет доступна команда
            }
        }

        public bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            return false;
        }
    }
}
