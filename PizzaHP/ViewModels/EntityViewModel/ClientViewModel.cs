using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private Client client;

        public ClientViewModel(Client cl)
        {
            client = cl;
        }

        public int ClientID
        {
            get { return client.ClientID; }
            set
            {
                client.ClientID = value;
                OnPropertyChanged("ClientID");
            }
        }

        public string FIO
        {
            get { return client.FIO; }
            set
            {
                client.FIO = value;
                OnPropertyChanged("FIO");
            }
        }

        public string PhoneNumber
        {
            get { return client.PhoneNumber; }
            set
            {
                client.PhoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string Email
        {
            get { return client.Email; }
            set
            {
                client.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public int N        //Кол-во заказов
        {
            get { return client.Order.Count(); }
        }
    }
}
