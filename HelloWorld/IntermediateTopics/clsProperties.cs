using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsProperties
    {
        public void runProperties()
        {
            //a property is a class member that encapusalets a getter and setter (with less code than 
            //the "AccessModifiersPerson" class

            var person = new PropertiesPerson();
                person.BirthDate = new DateTime(1982, 1, 1);
            Console.WriteLine(person.Age); ;
        }

    }
}
