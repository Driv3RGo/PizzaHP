using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PizzaHP.Models
{
    public class Result
    {
        public string Клиент { get; set; }
        public string Номер_заказа { get; set; }
        public string Количество_товара { get; set; }
        public TimeSpan Длительность_заказа { get; set; }
        public string Цена_заказа { get; set; }
    }

    public class ReportModel
    {
        public List<Result> Otchet(DateTime date1, DateTime date2)
        {
            SqlParameter param1 = new SqlParameter("@date1", date1);
            SqlParameter param2 = new SqlParameter("@date2", date2);
            PizzaContext db = new PizzaContext();
            var result = db.Database.SqlQuery<Result>("Zakaz @date1,@date2", new object[] { param1, param2 }).ToList();
            var data = result.Select(i => new Result()
            {
                Клиент = i.Клиент,
                Номер_заказа = i.Номер_заказа,
                Количество_товара = i.Количество_товара,
                Длительность_заказа = i.Длительность_заказа,
                Цена_заказа = i.Цена_заказа
            }).ToList();
            return data;
        }

        public string Pizza(DateTime date1, DateTime date2)
        {
            SqlParameter param1 = new SqlParameter("@date1", date1);
            SqlParameter param2 = new SqlParameter("@date2", date2);
            PizzaContext db = new PizzaContext();
            var pizza = db.Database.SqlQuery<string>("Pizza @date1,@date2", new object[] { param1, param2 }).SingleOrDefault();
            return pizza;
        }
    }   
}
