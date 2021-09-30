using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Convertors
{
   public class FixText
    {
        public static string FixTxt(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
