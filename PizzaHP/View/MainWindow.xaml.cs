using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaHP.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigable.Content = new Katalog();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)         //Кнопка закрыть
        {
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)          //Кнопка личный кабинет
        {
            LoginScreen ls = new LoginScreen();
            ls.Show();
        }

        private void Katalog_Click(object sender, RoutedEventArgs e)        //Конпка каталог
        {
            Grid1.Visibility = Visibility.Visible;
            Grid2.Visibility = Visibility.Hidden;
            Grid3.Visibility = Visibility.Hidden;
            Navigable.Content = new Katalog();
        }

        private void Konstruktor_Click(object sender, RoutedEventArgs e)    //Кнопка конструктор
        {
            Grid1.Visibility = Visibility.Hidden;
            Grid2.Visibility = Visibility.Visible;
            Grid3.Visibility = Visibility.Hidden;
            Navigable.Content = new Konstruktor();
        }

        private void Korzina_Click(object sender, RoutedEventArgs e)        //Кнопка корзина
        {
            Grid1.Visibility = Visibility.Hidden;
            Grid2.Visibility = Visibility.Hidden;
            Grid3.Visibility = Visibility.Visible;
            Navigable.Content = new Korzina();
        }
    }
}
