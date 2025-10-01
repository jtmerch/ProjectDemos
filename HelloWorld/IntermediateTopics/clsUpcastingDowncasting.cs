using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections;
using System.Collections.Generic;


namespace HelloWorld.IntermediateTopics
{
    class clsUpcastingDowncasting
    {

        //Upcasting is conversion from a derived class to a base class
        //Downcasting is conversion from a base class to a derived class
        //The "as" and "is" keywords

        public void runUpcastingDowncasting()
        {
            UpcastingDowncastingText text = new UpcastingDowncastingText();
            UpcastingDowncastingShape shape = text; //this is Upcasting because the base class is referencing the sub class. Both the "text" and "shape" object is referencing the same object in memory

            text.Width = 200;
            shape.Width = 100;

            Console.WriteLine(text.Width); //this will display 100 because text and shape reference the same object

            //Sample polymorphism
            //  StreamReader reader = new StreamReader(new MrmoryStream()); //in thise example you can pass any class that derives from the Stream Class


            //Upcasting example
            ArrayList list = new ArrayList(); //you shouldn't use ArrayList in real world appplications because it isn't TypeSafe
            list.Add(1); //you can pass any object to "Add" with no conversion required
            list.Add("Most");
            list.Add(new Text());

            var anotherList = new List<UpcastingDowncastingShape>(); //This is a "generic list" and it is type safe so it's better to use than ArrayList


            //Downcasting example
            UpcastingDowncastingShape shape2 = new UpcastingDowncastingText();
            UpcastingDowncastingText text2 = (UpcastingDowncastingText)shape2;

            //if you're not sure of the runtime type you shouln't cast, instead use the "as" keyword

            var testobject = text2 as UpcastingDowncastingShape;
            if (text2 != null)
            {
                //code
            }    

        }
    }
}
