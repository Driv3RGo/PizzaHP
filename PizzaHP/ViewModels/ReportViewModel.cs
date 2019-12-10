using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PizzaHP.Models;
using PizzaHP.View;

namespace PizzaHP.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private Page order;     //Страница заказы
        private Page client;    //Страница клиенты
        private Page ing;       //Страница ингредиенты
        private Page otchet;    //Страница отчёт

        PizzaContext db;
        ReportModel report;
        public List<OrderViewModel> Orders { get; set; }                    //заказы
        public ObservableCollection<Client> Clients { get; set; }           //клиенты

        private ObservableCollection<Ingredient> ingredients;       //ингредиенты
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged("Ingredients"); }
        }

        public List<Kategori> Kategori { get; set; }    //заполнение комбобокса с категориями

        private List<Result> data;      //Отчёт
        public List<Result> Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged("Data"); }
        }                    

        public ReportViewModel()
        {
            order = new Page_Order(this);
            client = new Page_Client(this);
            ing = new Page_Ing(this);
            otchet = new Page_Otchet(this);
            SourcePage = otchet;

            db = new PizzaContext();
            report = new ReportModel();
            Orders = db.Order.ToList().Select(i => new OrderViewModel(i)).ToList();
            Clients = new ObservableCollection<Client>(db.Client);
            Ingredients = new ObservableCollection<Ingredient>(db.Ingredient);
            Kategori = db.Kategori.ToList();
            Data = new List<Result>();
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

        private Page sourcePage;        //Выбранная страница
        public Page SourcePage
        {
            get { return sourcePage; }
            set { sourcePage = value; OnPropertyChanged("SourcePage"); }
        }

        public RelayCommand FilterCommand   //Открыть страницу
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    switch ((string)obj)
                    {
                        case "1": SourcePage = order; break;
                        case "2": SourcePage = client; break;
                        case "3": SourcePage = ing; break;
                        case "4": SourcePage = otchet; break;
                    } 
                }));
            }
        }   

        private RelayCommand deleteCommand;     //Удалить ингредиент из БД
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
                        ingredients.Remove(ing);
                        Ingredients = Ingredients;
                    }
                },
                (obj) => selecteding != null));  //условие, при котором будет доступна команда
            }
        }

        private RelayCommand changeCommand;     //Изменить информацию ингредиента из БД
        public RelayCommand ChangeCommand
        {
            get
            {
                return changeCommand ??
                (changeCommand = new RelayCommand(obj =>
                {
                    db.SaveChanges();
                },
                (obj) => selecteding != null));  //условие, при котором будет доступна команда
            }
        }

        private RelayCommand findCommand;   //Выполнить отчёт
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
                    }
                    else Sum = 0;
                    Data = report.Otchet(date_Start, date_End);
                    Pizza = report.Pizza(date_Start, date_End);
                },
                (obj) => date_End > date_Start));  //условие, при котором будет доступна команда
            }
        }
    }
}
