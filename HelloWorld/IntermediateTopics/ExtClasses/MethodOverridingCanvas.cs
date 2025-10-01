using HelloWorld.IntermediateTopics.ExtEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class MethodOverridingCanvas
    {
        public void DrawShapes(List<MethodOverridingShape> shapes)
        {
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}
