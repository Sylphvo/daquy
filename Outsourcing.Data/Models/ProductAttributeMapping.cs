using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ProductAttributeMapping : BaseEntity
    {
        public ProductAttributeMapping()
        {
            Value = "Default";
            IsRequired = false;
            DisplayOrder = 0;
        }

        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }

    }
}
