using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private Order order;

        public OrderViewModel(Order o)
        {
            order = o;
        }

        public string DataBegin
        {
            get { return order.DataBegin.ToString("dd MMMM yyyy | HH:mm:ss"); }
        }

        public string Products
        {
            get { return String.Join(",", order.Order_line.Select(i => i.Product.Name).ToList()); }
        }

        public string Cost
        {
            get { return Convert.ToString(order.Cost) + " " + "₽"; }
        }

        public string Status
        {
            get { return order.Status.Name; }
        }
    }
}
