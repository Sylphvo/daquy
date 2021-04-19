using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            DateCreated = DateTime.Now;
        }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone{ get; set; }
        public string CustomerEmail{ get; set; }
        public double OrderTotal { get; set; }
        public double TotalPayment { get; set; }
        public double Discount { get; set; }
        public string Temp_1 { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } 
    }
}
