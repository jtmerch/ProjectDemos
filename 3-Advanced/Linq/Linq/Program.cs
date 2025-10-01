using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {

            //  Func<int, int, int> add = (a, b) => a + b;
            // Console.WriteLine(add(2, 3));

            var books = new BookRepository().GetBooks();

            //Link Extension Methods
            
            books.Where();//filter and return a list of books based on a given condition
            books.Single(); //retrun a single that matches given condition
            books.SingleOrDefault(); //safer than Single because it return "null" if it cannot find a condition.

            books.First();
            books.FirstOrDefault();

            books.Last();
            boooks.LastOrDefault();

            books.Min();
            books.Max();

            books.Count();
            books.Sum();
            books.Average(b => b.Price);

            books.Skip(); //used for paging
            books.Take(); //also used for paging
        }

        private static float CalculateDiscount(float price)
        {
            return 0;
        }

    
    }
}
