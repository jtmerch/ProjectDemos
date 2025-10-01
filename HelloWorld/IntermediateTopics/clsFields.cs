using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsFields
    {
        public void runFields()
        {

            var customer = new FieldsCustomer(1);
            customer.Orders.Add(new FieldsOrder());
            customer.Orders.Add(new FieldsOrder());

            customer.Promote(); //accidently reinitizlized fields so you need to use a "readonly" modifier when defining the list

            Console.WriteLine(customer.Orders.Count);
        }
    }
}
