using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class Extention
    {
        public static SecureString ToSecureString(this string pwd)
        {
            var secureString = new SecureString();
            foreach (char c in pwd)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }
    }
}
