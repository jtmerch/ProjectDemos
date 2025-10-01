using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class MethodOverridingCircle : MethodOverridingShape
    {

        // you can add extra properties about the Circle here

        public override void Draw()
        {
            //any code specific to circle class before passing execution to Draw()
            //   base.Draw(); // this gets auto generated and oints to the inherited Shape class

            Console.WriteLine("Draw a circle");
        }
    }
}
