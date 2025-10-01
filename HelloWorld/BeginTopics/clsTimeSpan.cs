using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Topics
{
    class clsTimeSpan
    {
        public clsTimeSpan() //constructor
        {
           
        }

        public void runTimeSpan()
        {
            TimeSpan initTimeSpan = new TimeSpan(1, 2, 3);

            TimeSpan.FromHours(1); //another wayh to write initTimeSpan

            var stateDate = DateTime.Now;
            var endDate = DateTime.Now.AddMinutes(2);
            var duration = endDate - stateDate;

            Console.WriteLine("Duration: " + duration);

            //Properties
            Console.WriteLine("Minutes: " + initTimeSpan.Minutes); // returns "2" from initTimeSpan
            Console.WriteLine("Total Minutes: " + initTimeSpan.TotalMinutes);

            //Add
            Console.WriteLine("Add Example: " + initTimeSpan.Add(TimeSpan.FromMinutes(8)));
            //Subtract
            Console.WriteLine("Subtract Example: " + initTimeSpan.Subtract(TimeSpan.FromMinutes(2)));

            //ToString()
            Console.WriteLine("ToString: " + initTimeSpan.ToString());

            //Parse (to convert from String)
            Console.WriteLine("Parse: " + TimeSpan.Parse("01:02:03"));
        }
    }
}
