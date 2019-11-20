using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ShowPizza : INotifyPropertyChanged
    {
        PizzaContext db;
        public List<Product> allProduct { get; set; }

        public ShowPizza()
        {
            db = new PizzaContext();
            allProduct = db.Product.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
