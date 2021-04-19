using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            DateCreated = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address_Crypto { get; set; }
        public string Balance_ETH { get; set; }
        public string Balance_VIP { get; set; }
        public string ParentId { get; set; }

        public string Phone { get; set; }
        public string IP_login { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool Activated { get; set; }

        public Gender Gender { get; set; }

        public SystemRoles RoleId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool Deleted { get; set; }
        public string PasswordNotHash { get; set; }
        public string TotalBets { get; set; }
        public string SystemBets { get; set; }
        public string Address_VIP { get; set; }
        public string Temp { get; set; }//lưu QR code image
        public string Temp1 { get; set; }// lưu QR code text
        public string Temp2 { get; set; }//lưu password change
        public string Temp3 { get; set; }
        public string DisplayName
        {
            get { return LastName + " " + FirstName; }
        }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
    public enum SystemRoles
    {
        [Description("Admin")]
        Role01 = 0
    }
    public enum Gender
    {
        [Description("Nam")]
        Male = 0,
        [Description("Nữ")]
        Female = 1
    }
}
