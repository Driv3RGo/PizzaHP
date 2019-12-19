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
        private Page product;    //Страница продуктов
        private Page ing;       //Страница ингредиенты
        private Page otchet;    //Страница отчёт

        PizzaContext db;
        ReportModel report;     //Отчёт

        private ObservableCollection<Product> products;       //продукты
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged("Products"); }
        }

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
            product = new Page_Product(this);       //Страница продуктов 
            ing = new Page_Ing(this);               //Страница ингредиентов
            otchet = new Page_Otchet(this);         //Страница с отчётом
            SourcePage = otchet;                    //Текущая страница

            db = new PizzaContext();
            report = new ReportModel();             //Отчёт
            Products = new ObservableCollection<Product>(db.Product);
            Ingredients = new ObservableCollection<Ingredient>(db.Ingredient);
            Kategori = db.Kategori.ToList();        //Заполнение комбобокса в ингредиентах
            Data = new List<Result>();              //Результат отчёта
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

        private Product selectedproduct = null;       //Выбранный продукт
        public Product SelectedProduct
        {
            get { return selectedproduct; }
            set { selectedproduct = value; OnPropertyChanged("SelectedProduct"); }
        }

        private Ingredient selecteding = null;       //Выбранный ингредиент
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
                        case "1": SourcePage = product; break;
                        case "2": SourcePage = ing; break;
                        case "3": SourcePage = otchet; break;
                    } 
                }));
            }
        }

        private RelayCommand ddeleteCommand;     //Удалить продукт из БД
        public RelayCommand DDeleteCommand
        {
            get
            {
                return ddeleteCommand ??
                (ddeleteCommand = new RelayCommand(obj =>
                {
                    Product pr = db.Product.Find(selectedproduct.ProductID);
                    if (ing != null)
                    {
                        products.Remove(pr);
                        Products = Products;
                    }
                },
                (obj) => selectedproduct != null));  //условие, при котором будет доступна команда
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
                (obj) => selecteding != null || selectedproduct != null));  //условие, при котором будет доступна команда
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
