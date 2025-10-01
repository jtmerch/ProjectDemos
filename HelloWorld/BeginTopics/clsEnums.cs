using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsEnums
    {
        public void runEnums()
        {
            var shippingMethod = ShippingMethods.Express;
            Console.WriteLine((int)shippingMethod);

            int recMethod = 1;
            Console.WriteLine((ShippingMethods)recMethod);

            Console.WriteLine(shippingMethod.ToString());

            string methodName = "Express";
            var parsedShippingMethod = (ShippingMethods)Enum.Parse(typeof(ShippingMethods), methodName);

            Console.WriteLine(parsedShippingMethod);

        
        }

        enum ShippingMethods
        {
            RegularAirMail = 0,
            RegisteredAirMail = 1,
            Express = 2
        }

    }
}
