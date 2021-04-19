using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Outsourcing.Data.Utils
{
    public static class Utils
    {
        public static string SaveFile(string pathSv, HttpPostedFileBase file,int Type)
        {
            try
            {
                string mimeType = file.ContentType.Split('/').LastOrDefault();
                var url = "";
                switch (Type)
                {
                    case (int)Enums.LibraryImageType.Image:
                        url = "Uploads/Images/" + Guid.NewGuid().ToString() + "." + mimeType;
                        break;
                    case (int)Enums.LibraryImageType.Video :
                        url = "Uploads/Video/" + Guid.NewGuid().ToString() + "." + mimeType;
                        break;
                    case (int)Enums.LibraryImageType.Course:
                        url = "Uploads/Video/" + Guid.NewGuid().ToString() + "." + mimeType;
                        break;
                    default:
                        break;
                }
                var path = Path.Combine(pathSv, url);
                file.SaveAs(path);
                return "/" + url;
            }
            catch
            {
                return null;
            }
        }

    }
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum value)
        {
            try
            {
                Type enumType = value.GetType();
                var enumValue = Enum.GetName(enumType, value);
                MemberInfo member = enumType.GetMember(enumValue)[0];

                var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                var outString = ((DisplayAttribute)attrs[0]).Name;

                if (((DisplayAttribute)attrs[0]).ResourceType != null)
                {
                    outString = ((DisplayAttribute)attrs[0]).GetName();
                }

                return outString;
            }
            catch (Exception e)
            {
                //Utils._log.Error(e);
                return string.Empty;
            }
        }
    }
}
