using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsMethodOverriding
    {
        public void runMethodOverriding()
        {
            //Method overriding is modifying the implementation of an inherited method

            var shapes = new List<MethodOverridingShape>();
            shapes.Add(new MethodOverridingCircle());
            shapes.Add(new MethodOverridingRectangle());

            var canvas = new MethodOverridingCanvas();
            canvas.DrawShapes(shapes);
        }
        }
}
