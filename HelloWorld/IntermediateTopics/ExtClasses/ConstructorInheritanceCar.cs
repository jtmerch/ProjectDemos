using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
   public class ConstructorInheritanceCar : ConstructorInheritanceVehicle
    {
        public ConstructorInheritanceCar(string registrationNumber) //constructor
            :base(registrationNumber)
        {
            Console.WriteLine("car is being initialized {0}", registrationNumber);
        }
    }
}
