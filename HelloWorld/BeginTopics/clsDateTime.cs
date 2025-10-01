using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Topics
{
    class clsDateTime
    {
        
        public void runDateTime()
        {
            var dateTime = new DateTime(2015, 1, 1);
            var nowDate = DateTime.Now;
            var todayDate = DateTime.Today;

            Console.WriteLine("Hour " + nowDate.Hour);
            Console.WriteLine("Minute " + nowDate.Minute);

            var tomorrow = nowDate.AddDays(1);
            var yesterday = nowDate.AddDays(-1);

            Console.WriteLine(nowDate.ToLongDateString());
            Console.WriteLine(nowDate.ToShortDateString());
            Console.WriteLine(nowDate.ToLongTimeString());
            Console.WriteLine(nowDate.ToShortTimeString());
            Console.WriteLine(nowDate.ToString("yyyy-MM-dd HH:mm"));
        }
    }
}
