using Outsourcing.Data.Utils;
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
    public class LibraryImage : BaseEntity
    {
        public LibraryImage()
        {
            this.CreateDate = DateTime.Now;
            this.Status = (int)Enums.LibraryImageStatus.Active;
       
        }
        public int Type { get; set; }
        public string Url { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Temp1 { get; set; }
        public String Temp2 { get; set; }
        public int? Temp3 { get; set; }
        public String Temp4 { get; set; }
        public String Temp5 { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
    }

}
