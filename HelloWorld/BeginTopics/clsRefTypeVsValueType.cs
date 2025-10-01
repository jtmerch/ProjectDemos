using HelloWorld.BeginClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsRefTypeVsValueType
    {
        public void runRefTypeVsValueType()
        {
           

           // value type
              int a = 10;
            int b = a;
            b++;
            Console.WriteLine(string.Format("a: {0} b: {1}", a, b));

            //reference type
            var array1 = new int[3]
            {
                1,
                2,
                3
            };
            var array2 = array1;
            array1[0] = -1;
            //array1 is stored on a heap and the stack points to the heap memory address, 
            // so array2 actually modifies array 1(because it also points to the heap), 
            // that is what makes it a reference type
            Console.WriteLine(string.Format("array1 first val: {0} array2 first val: {1}", array1[0], array2[0]));

            //  more Reference Type vs Value Type
            //Value Type:
            int number = 1;
            Increment(number);
            Console.WriteLine(number); //number will still be 1

            // Refernce Type:
            var person = new Person()
            {
                Age = 20
            };
            MakeOld(person);
            Console.WriteLine(person.Age); //age will incr to 30
        }


        public static void Increment(int number)
        {
            number += 10;
        }

        public static void MakeOld(Person person)
        {
            person.Age += 10;
        }
    }
}
