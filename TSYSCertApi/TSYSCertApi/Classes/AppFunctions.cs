using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TSYSCertApi.Classes
{
    public static class AppFunctions
    {

        public static string CleanUpSummitXML(string txt)
        {
            //txt.TrimEnd('\0');
            //txt.Trim();
            //txt = txt.Replace("\0", string.Empty);
            // txt = txt.Replace(" ", string.Empty);
            txt.TrimStart(' ');
            txt.TrimEnd(' ');
            txt = txt.Replace("I4.", string.Empty);
            txt = txt.Replace("I2.", string.Empty);
            txt = txt.Substring(0, txt.Length - 1);

            return ReplaceHexadecimalSymbols(txt);
        }
      public static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }
    }
}
