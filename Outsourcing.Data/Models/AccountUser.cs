using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class AccountUser:BaseEntity
    {
        public AccountUser()
        {
            this.dateCreated = DateTime.Now;
            this.Deleted = false;
            this.IsActive = true;
        }
        public string Hoten { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string Tendn { get; set; }
        public string Mk { get; set; }
        public DateTime dateCreated { get; set; }
        public bool Deleted { get; set; }
        public bool IsActive { get; set; }
    }
}
