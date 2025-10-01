using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Topics
{
    class clsLists
    {
        public void runLists()
        {
            var numbers = new List<int>()
              {
                  1, 2, 3, 4
              }; //brackets<> mean that this is a generic type and you specifiy the actual type inside the brackets
            numbers.Add(1); //adds one object, in this case it is an int because List is init to "<int>
            numbers.AddRange(new int[3] { 5, 6, 7 });

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();
            Console.WriteLine("Index of 1: " + numbers.IndexOf(1));

            Console.WriteLine();
            Console.WriteLine("Last index of 1: " + numbers.LastIndexOf(1));

            Console.WriteLine("Count: " + numbers.Count);

            //  numbers.Remove(1); //removes the first "1"
            for (var i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == 1)
                {
                    numbers.Remove(numbers[i]); //remvoes all with that number
                }

            }
            Console.WriteLine();
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            numbers.Clear(); //removes all elements from list
            Console.WriteLine("Count: " + numbers.Count);
        }
    }
}
