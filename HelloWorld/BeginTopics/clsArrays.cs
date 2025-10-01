using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsArrays
    {
        public void runArrays()
        {
            int[] numbers = new int[] { 3, 7, 9, 2, 14, 6 };

            //Length
            Console.WriteLine("Lenght: " + numbers.Length); //check numbers lenght not array lenght because this object is static. Array class is reference and doesn't have that option

            //IndexOf() returns position of array (in form of int)
            var index = Array.IndexOf(numbers, 9);
            Console.WriteLine(index);

            //Clear()
            Array.Clear(numbers, 0, 2); //sets numbers to 0, sets bool to false, sets strings to null

            Console.WriteLine("-----Clear()");
            foreach (var number in numbers)
            {

                Console.WriteLine(number);
            }

            //Copy()
            int[] another = new int[3];
            Array.Copy(numbers, another, 3);

            Console.WriteLine("-----Copy()");
            foreach (var number in another)
            {

                Console.WriteLine(number);
            }

            //Sort()
            Array.Sort(numbers);

            Console.WriteLine("-----Sort()");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            //Reverse()
            Array.Reverse(numbers);

            Console.WriteLine("-----Reverse()");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
