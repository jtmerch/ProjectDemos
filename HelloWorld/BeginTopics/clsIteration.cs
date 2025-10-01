using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsIteration
    {
        public void runIteration()
        {
            for (var i = 1; i <= 10; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            for (var i = 10; i >= 1; i--)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            var numbers = new int[] { 1, 2, 3, 4 };
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var iWhile = 0;
            while (iWhile <= 10)
            {
                if (iWhile % 2 == 0)
                {
                    Console.WriteLine(iWhile);
                }
                iWhile++;
            }

            while (true)
            {
                Console.Write("Type your name: ");
                string input = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("@Echo: " + input);

                    continue; //continue to beginning of loop
                }

                break;
            }

        }
    }
}
