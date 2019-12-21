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
using PizzaHP.ViewModels;

namespace PizzaHP.View
{
    /// <summary>
    /// Логика взаимодействия для Page_Otchet.xaml
    /// </summary>
    public partial class Page_Otchet : Page
    {
        public Page_Otchet(ReportViewModel report)
        {
            InitializeComponent();
            DataContext = report;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(grid, "Распечатываем отчёт");
            }
        }
    }
}
