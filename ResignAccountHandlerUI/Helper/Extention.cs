using System.Security;

namespace ResignAccountHandlerUI
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
        public static bool HasMoreThanOneFlag(this FormType t)
        {
            return (t & (t - 1)) != 0;
        }
    }
}
