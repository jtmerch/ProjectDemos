using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsConstructorInheritance
    {
        public void runConstructorInheritance()
        {
            string registrationNumber = "123456";
            var car = new ConstructorInheritanceCar(registrationNumber); //constructor of base class will always be inherited first (in this case it is the Vehicle class). So use ": base"

        }

    }
    }
