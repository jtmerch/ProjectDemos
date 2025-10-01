using System;
using System.Collections;
using System.Collections.Generic;

namespace HelloWorld.IntermediateTopics
{
    class clsBoxingUnboxing
    {
        public void runBoxingUnboxing()
        {
            /*Boxing is the process of converting a value type instance to an object reference (Has a performance penalty)
            Example:
            int number = 10;
            object obj = number;

            Unboxing is the opposite of Boxing (Has a performance penalty)
            Example:
            object obj = 10;
            int number = int(obj);
            */

            ArrayList list = new ArrayList(); //no type safety
            list.Add(1);//boxing
            list.Add("Joe");
            list.Add(DateTime.Today);

            var number = (int) list[1]; //unboxing


            //An example of using a generic list 
            var anotherList = new List<int>(); //no boxing and unboxing because every element will be of type <int>
            anotherList.Add(1);
        }
    }
}
