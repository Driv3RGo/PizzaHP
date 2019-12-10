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

        public int OrderID
        {
            get { return order.OrderID; }
            set
            {
                order.OrderID = value;
                OnPropertyChanged("OrderID");
            }
        }

        public DateTime DataBegin
        {
            get { return order.DataBegin; }
            set
            {
                order.DataBegin = value;
                OnPropertyChanged("DataBegin");
            }
        }

        public string Client_FK
        {
            get { return order.Client.FIO; }
            set
            {
                order.Client.FIO = value;
                OnPropertyChanged("Client_FK");
            }
        }

        public decimal Cost
        {
            get { return order.Cost; }
            set
            {
                order.Cost = value;
                OnPropertyChanged("Cost");
            }
        }

        public string Status_FK
        {
            get { return order.Status.Name; }
            set
            {
                order.Status.Name = value;
                OnPropertyChanged("Status_FK");
            }
        }

        public DateTime? DataEnd
        {
            get { return order.DataEnd; }
            set
            {
                order.DataEnd = value;
                OnPropertyChanged("DataEnd");
            }
        }

        public string Address
        {
            get { return order.Address; }
            set
            {
                order.Address = value;
                OnPropertyChanged("Address");
            }
        }

        public string OrderedPizza
        {
            get { return String.Join(",", order.Order_line.Select(i => i.Product.Name).ToList()); }
        }
    }
}
