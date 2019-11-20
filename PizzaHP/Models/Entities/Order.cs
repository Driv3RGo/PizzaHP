namespace PizzaHP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_line = new HashSet<Order_line>();
        }

        public int OrderID { get; set; }

        public DateTime DataBegin { get; set; }

        public int Client_FK { get; set; }

        public decimal Cost { get; set; }

        public int Status_FK { get; set; }

        public DateTime? DataEnd { get; set; }

        public decimal? Skidka { get; set; }

        public int? Staff_FK { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_line> Order_line { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Status Status { get; set; }
    }
}
