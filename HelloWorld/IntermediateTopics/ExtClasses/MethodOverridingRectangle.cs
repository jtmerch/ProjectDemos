using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class MethodOverridingRectangle : MethodOverridingShape
    {
        // you can add extra properties about the Rectangle here

        public override void Draw()
        {
            //put any code specific to rectangle class before passing execution to Draw()
            //   base.Draw(); // this gets auto generated and points to the inherited Shape class

            Console.WriteLine("Draw a rectangle");

        }
    }
}
