using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsString
    {
        public void runString()
        {

            var fullName = "Joe Thompson ";
            Console.WriteLine("Trim: '{0}'", fullName.Trim());
            Console.WriteLine("Trim and To Upper: '{0}'", fullName.Trim().ToUpper()); //you can chain the methods because each chain returns a new string (Making them "Immutable")

            //grab to first and last name using Substring
            var delimIndex = fullName.IndexOf(' ');
            var firstName = fullName.Substring(0, delimIndex);
            var lastName = fullName.Substring(delimIndex + 1);

            Console.WriteLine("First name: " + firstName);
            Console.WriteLine("Last name: " + lastName);

            //same thing using split method
            var names = fullName.Split(' ');
            Console.WriteLine("First name: " + names[0]);
            Console.WriteLine("Last name: " + names[1]);

            //replace
            Console.WriteLine("Replaced name: " + fullName.Replace("Joe", "JT"));

            //validate null or empty
            if (String.IsNullOrWhiteSpace(" ")) //used to be IsNullOrEmpty but that allowed white space
            {
                Console.WriteLine("Invalid");
            }

            //convert str to number
            string strAge = "25";
            byte byteAge = Convert.ToByte(strAge); //instead of ToInt32 you can convert age ToByte because no one can be older than 256 years old
            Console.WriteLine(byteAge);

            //convert from num to string
            float price = 29.95f;
            Console.WriteLine(price.ToString("C"));

        }
    }
}
