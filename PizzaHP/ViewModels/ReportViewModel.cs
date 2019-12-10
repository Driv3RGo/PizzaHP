using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        PizzaContext db;
        Models.ReportModel report;
        public List<OrderViewModel> Orders { get; set; }                    //заказы
        public ObservableCollection<Client> Clients { get; set; }           //клиенты
        public ObservableCollection<Ingredient> Ingredients { get; set; }   //ингредиенты

        private List<Result> data;
        public List<Result> Data        //Отчёт
        {
            get { return data; }
            set { data = value; OnPropertyChanged("Data"); }
        }                    

        public ReportViewModel()
        {
            db = new PizzaContext();
            report = new Models.ReportModel();
            Orders = db.Order.ToList().Select(i => new OrderViewModel(i)).ToList();
            Clients = new ObservableCollection<Client>(db.Client);
            Ingredients = new ObservableCollection<Ingredient>(db.Ingredient);
            Data = new List<Result>();
        }

        private double orderOpacity = 1.0;      //Прозрачность grid, который содержит информации о заказах
        public double OrderOpacity
        {
            get
            {
                return orderOpacity;
            }
            set
            {
                orderOpacity = value;
                OnPropertyChanged("OrderOpacity");
            }
        }

        private double clientOpacity = 0.0;      //Прозрачность grid, который содержит информации о клиентах
        public double ClientOpacity
        {
            get
            {
                return clientOpacity;
            }
            set
            {
                clientOpacity = value;
                OnPropertyChanged("ClientOpacity");
            }
        }

        private double ingOpacity = 0.0;      //Прозрачность grid, который содержит информации о ингредиентах
        public double IngOpacity
        {
            get
            {
                return ingOpacity;
            }
            set
            {
                ingOpacity = value;
                OnPropertyChanged("IngOpacity");
            }
        }
        
        private double reportOpacity = 0.0;      //Прозрачность grid, который содержит отчёт
        public double ReportOpacity
        {
            get
            {
                return reportOpacity;
            }
            set
            {
                reportOpacity = value;
                OnPropertyChanged("ReportOpacity");
            }
        }

        private DateTime date_Start = DateTime.Now;           //Начальная дата
        public DateTime Date_Start
        {
            get { return date_Start; }
            set { date_Start = value; OnPropertyChanged("Date_Start"); }
        }

        private DateTime date_End = DateTime.Now.AddYears(1); //Конечная дата
        public DateTime Date_End
        {
            get { return date_End; }
            set { date_End = value; OnPropertyChanged("Date_End"); }
        }

        private int n;          //Кол-во заказов
        public int N
        {
            get { return n; }
            set { n = value; OnPropertyChanged("N"); }
        }

        private string pizza;          //Популярная пицца
        public string Pizza
        {
            get { return pizza; }
            set { pizza = value; OnPropertyChanged("Pizza"); }
        }

        private decimal sum;          //Выручка
        public decimal Sum
        {
            get { return sum; }
            set { sum = value; OnPropertyChanged("Sum"); }
        }

        private Ingredient selecteding = null;       //Выбранный продукт
        public Ingredient SelectedIng
        {
            get { return selecteding; }
            set { selecteding = value; OnPropertyChanged("SelectedIng"); }
        }

        public RelayCommand FilterCommand
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    OrderOpacity = 0.0;
                    ClientOpacity = 0.0;
                    IngOpacity = 0.0;
                    ReportOpacity = 0.0;
                    switch ((string)obj)
                    {
                        case "1": OrderOpacity = 1.0; break;
                        case "2": ClientOpacity = 1.0; break;
                        case "3": IngOpacity = 1.0; break;
                        case "4": ReportOpacity = 1.0; break;
                    } 
                }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                (deleteCommand = new RelayCommand(obj =>
                {
                    Ingredient ing = db.Ingredient.Find(selecteding.IngredientID);
                    if (ing != null)
                    {
                        db.Ingredient.Remove(ing);
                    }
                },
                (obj) => selecteding != null));  //условие, при котором будет доступна команда
            }
        }

        private RelayCommand findCommand;
        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ??
                (findCommand = new RelayCommand(obj =>
                {
                    N = db.Order.Where(i => i.DataEnd >= date_Start && i.DataEnd <= date_End).Count();
                    if (N > 0) //Если есть хотя бы 1 заказ
                    {
                        Sum = db.Order.Where(i => i.DataEnd >= date_Start && i.DataEnd <= date_End).Sum(i => i.Cost);
                        Data = report.Otchet(date_Start, date_End);
                        Pizza = report.Pizza(date_Start, date_End);
                    }
                },
                (obj) => date_End > date_Start));  //условие, при котором будет доступна команда
            }
        }
    }
}
