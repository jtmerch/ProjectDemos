using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsClassesFirst
    {
       
        public void runClassesFirst()
        {
            var person = Person.Parse("John");
            person.Introduce("Joe");

        }

     
    }
}
