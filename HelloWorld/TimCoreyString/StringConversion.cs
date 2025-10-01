using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.TimCoreyString
{
    class StringConversion
    {
       public void ConvertString()
        {
            string testString = "this is the FBI calling!";

            TextInfo currenTextInfo = CultureInfo.CurrentCulture.TextInfo;
            string result;

            result = testString.ToLower();
            Console.WriteLine(result);

            result = testString.ToUpper();
            Console.WriteLine(result);

            result = currenTextInfo.ToTitleCase(testString);
            Console.WriteLine(result);
        }
    }
}
