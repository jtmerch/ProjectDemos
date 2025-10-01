using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    public class AccessModifiersMoreCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Promote()
        {
            var rating = CalculateRating(excludeOrders: true);
            if (rating == 1)
            {
                Console.WriteLine("promote to level 1");
            }
            else
            {
                Console.WriteLine("promote to level 2");
            }

        }

        protected int CalculateRating(bool excludeOrders)
        {
            return 1;
        }

    }
}
