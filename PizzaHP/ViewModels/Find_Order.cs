using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaHP.Models;

namespace PizzaHP.ViewModels
{
    public class Find_Order : ViewModelBase
    {
        PizzaContext db;
        private List<OrderViewModel> allOrder;
        public List<OrderViewModel> AllOrder
        {
            get { return allOrder; }
            set { allOrder = value; OnPropertyChanged("AllOrder"); }
        }

        public Find_Order()
        {
            db = new PizzaContext();
        }

        private string phoneNumber = "";      //Телефон клиента
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        private RelayCommand findCommand;   //Поиск заказов
        public RelayCommand FindCommand
        {
            get
            {
                return findCommand ??
                (findCommand = new RelayCommand(obj =>
                {
                    AllOrder = db.Order.Where(i => i.Client.PhoneNumber == phoneNumber).ToList().Select(i => new OrderViewModel(i)).ToList();
                },
                (obj) => phoneNumber != ""));  //условие, при котором будет доступна команда
            }
        }
    }
}
