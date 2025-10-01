using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    public class AccessModifiersMoreGoldCustomer : AccessModifiersMoreCustomer
    {
        public void OfferVoucher()
        {
            this.CalculateRating(excludeOrders: true); //cannot see CalculateRating if private, can see if it is protected.  Avoid protected unless you have a real reason such as the concept of CalculateRating is very stable in the business logic
        }

    }
}
