using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
   public class Person
    {
        public string Name;

        public void Introduce(string To)
        {
            Console.WriteLine("Hi {0}, I am {1}.", To, Name);
        }

        public static Person Parse(string str)
        {
            var person = new Person();
            person.Name = str;

            return person;
        }
    }
}
