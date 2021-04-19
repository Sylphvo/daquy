using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Utils
{
    public class Enums
    {
        public enum LibraryImageType
        {
            Image = 1,
            Video = 2,
            Course = 3,
        }
        public enum LibraryImageStatus
        {
            Active = 1,
            Deactive = 2
        }
        public enum VideoEnum
        {
            [Display(Name = "Nhúng youtube")]
            Youtube = 1,
            [Display(Name = "Tải video lên")]
            Server = 2,
        }
        public enum ImageEnum
        {
            [Display(Name = "Thư viện")]
            Library = 4,
            [Display(Name = "Sơ cấp")]
            Primary = 1,
            [Display(Name = "Trung cấp")]
            Secondary = 2,
            [Display(Name = "Cao cấp")]
            High = 3,

        }
    }
}
