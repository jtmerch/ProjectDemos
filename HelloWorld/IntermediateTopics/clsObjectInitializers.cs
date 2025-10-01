using HelloWorld.IntermediateTopics.ExtClasses;
using System;


namespace HelloWorld.IntermediateTopics
{
    class clsObjectInitializers
    {
        public void runObjectInitializers()
        {
            //"out" test
          //int number = int.Parse("abc"); //returns exception

            //NOTE: "out" is not recommended, try to find another way (like maybe "try/catch" block).
            int number;
          var result = int.TryParse("abc", out number); //out modifier enables this to return true or values, it does not trow an exception

            if (result == true)
            {
                Console.WriteLine(number);
            }
            else
            {
                Console.WriteLine("conversion failed");
            }

        }

        static void UseParams()
        {
            var calculator = new Calculator();
            Console.WriteLine(calculator.Add(1, 2)); //using the "params" keyword in the calss enable me to do this.
            Console.WriteLine(calculator.Add(1, 2, 3));
            Console.WriteLine(calculator.Add(1, 2, 3, 4));

            Console.WriteLine(calculator.Add(new int[] { }));
        }

        static void UsePoints() //just to separate points functions
        {
            try
            {

                //Overloading method examples:
                var point = new Point(10, 20);
                point.Move(new Point(40, 60));
                Console.WriteLine("Point is at ({0}, {1})", point.X, point.Y);

                point.Move(new Point(100, 200));
                Console.WriteLine("Point is at ({0}, {1})", point.X, point.Y);

            }
            catch (Exception ex)
            {

                Console.WriteLine("An unexpected error ccurred: " + ex.Message);
            }

        }

    
    }

   
}
