using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class UserProductsMapping : BaseEntity
    {
        public UserProductsMapping()
        {
            this.CreateDate = DateTime.Now;
       
        }
        public int ProductID { get; set; }
        public string UserId { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDeleted { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }



        

    }

}
