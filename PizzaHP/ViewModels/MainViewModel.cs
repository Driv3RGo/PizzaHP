using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PizzaHP.Models;
using PizzaHP.View;

namespace PizzaHP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private PizzaContext db;
        private ShowBasket basket;  //Корзина
        private Page menu;          //Страница меню
        private Page build;         //Страница конструктора
        private Page kor;           //Страница корзины
        private Page report;        //Страница для персонала
        private Page order;         //Страница заказов
        private LoginScreen loginScreen;    //Окно с авторизацией

        public MainViewModel()
        {
            db = new PizzaContext();
            basket = new ShowBasket();

            menu = new Katalog(basket);
            build = new Konstruktor(basket);
            kor = new Korzina(basket);
            report = new Page_Report();
            order = new Page_Order();

            SourcePage = menu;      //Текущая страница
        }

        private Page sourcePage;    //Выбранная страница
        public Page SourcePage
        {
            get { return sourcePage; }
            set { sourcePage = value; OnPropertyChanged("SourcePage"); }
        }

        private string login;       //Логин
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged("Login"); }
        }

        public RelayCommand FilterCommand   //Открыть страницу
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    switch ((string)obj)
                    {
                        case "1": SourcePage = menu; break;
                        case "2": SourcePage = build; break;
                        case "3": SourcePage = kor; break;
                        case "4":
                            {
                                Login = null;
                                loginScreen = new LoginScreen(this);
                                loginScreen.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                                loginScreen.Show();
                            }
                            break;
                        case "5": SourcePage = order; break;
                    }
                }));
            }
        }

        public RelayCommand SigninCommand   //Войти
        {
            get
            {
                return new RelayCommand((obj =>
                {
                    var passwordBox = obj as PasswordBox;
                    if (passwordBox == null)
                        return;
                    var password = passwordBox.Password;
                    Staff st = db.Staff.Where(i => i.Login == login).SingleOrDefault();
                    if(st != null && st.Password == password)
                    {
                        SourcePage = report;
                        loginScreen.Close();
                    }
                    else DisplayName = "Неверный логин или пароль";
                }));
            }
        }
    }
}
