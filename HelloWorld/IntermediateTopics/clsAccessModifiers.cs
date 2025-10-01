using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsAccessModifiers
    {
        public void runAccessModifiers()
        {
            //test
            var person = new AccessModifiersPerson();
            person.SetBirthDate(DateTime.Today.AddYears(-30));
            Console.WriteLine(person.GetBirthdate());

        }
    }
}
