using HelloWorld.BeginEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsConditional
    {
        public void runConditional()
        {
            bool isGoldCustomer = true;
            float price;
            if (isGoldCustomer)
            {
                price = 19.95f;
            }
            else
            {
                price = 29.95f;
            }
            Console.WriteLine("first price " + price);

            float price2 = (isGoldCustomer) ? 19.95f : 29.95f; //conditional operation version of above
            Console.WriteLine(price2);

           // switch
             var season = Season2.Autumn;

            switch (season)
            {
                case Season2.Spring:
                    break;
                case Season2.Summer:
                case Season2.Autumn:
                    Console.WriteLine("It's summer or automn and a beautiful season");
                    break;
                case Season2.Winter:
                    break;

                default:
                    Console.WriteLine("No season");
                    break;
            }
            //------------------------------


            
        }
    }
}
