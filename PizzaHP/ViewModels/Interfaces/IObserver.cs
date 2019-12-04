using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHP.ViewModels
{
    public interface IObserver  //Наблюдатель
    {
        void Update(Object ob);     //уведомление наблюдателю
    }
}
