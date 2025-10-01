using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    public class ConstructorInheritanceVehicle
    {
        private readonly string _registrationNumber;
        public ConstructorInheritanceVehicle() //constructor
        {
            Console.WriteLine("Vehicle is being initialized");
        }

        public ConstructorInheritanceVehicle(string registrationNumber)
        {
            _registrationNumber = registrationNumber;

            Console.WriteLine("Vehicle is being initialized {0}", registrationNumber);
        }
    }
}
