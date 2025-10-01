using HelloWorld.BeginClasses;
using HelloWorld.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsObject
    {
        public void runObject()
        {
            Person john = new Person();
            john.FirstName = "John";
            john.LastName = "Smithf";
            john.Introduce();

            Calculator calculator = new Calculator();
            var result = calculator.Add(5, 6);
            Console.WriteLine(result);

          //  ----------new OBJECT---------------------- -

            var numbers = new int[3];
            numbers[0] = 1;

            Console.WriteLine(numbers[0]);
            Console.WriteLine(numbers[1]);
            Console.WriteLine(numbers[2]);


            var flags = new bool[3];
            flags[0] = true;

            Console.WriteLine(flags[0]);
            Console.WriteLine(flags[1]);
            Console.WriteLine(flags[2]);

            var names = new string[3]{
                "Jack",
                "John",
                "Mary"
            };

            string firstName = "Joe";
            string lastName = "Thompson";

            var fullName = firstName + " " + lastName;

            var fullName2 = String.Format("My name is {0} {1}", firstName, lastName);


            var names2 = new string[3]
            {
                            "John",
                            "Jack",
                            "Mary"
            };
            var formattedNames = string.Join(", ", names2);

            Console.WriteLine(formattedNames);

            var text = @"Hi John
            Look into the following patchs 
            c:\testdirectory\newtest";
            Console.WriteLine(text);

        }
    }
}
