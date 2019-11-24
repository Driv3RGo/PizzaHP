namespace PizzaHP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Composition")]
    public partial class Composition
    {
        public int ID { get; set; }

        public int Product_FK { get; set; }

        public int Ingredient_FK { get; set; }

        public int Count { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public virtual Product Product { get; set; }
    }
}
