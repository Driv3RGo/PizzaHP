using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHP.ViewModels
{
    public interface IObservable    //Наблюдаемый объект
    {
        void AddObserver(IObserver o);      //добавление наблюдателя
        void NotifyObservers();             //уведомление наблюдателя
    }
}
