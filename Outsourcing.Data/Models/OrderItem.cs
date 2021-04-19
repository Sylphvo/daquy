using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public  class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } 
        public string Temp_1 { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
