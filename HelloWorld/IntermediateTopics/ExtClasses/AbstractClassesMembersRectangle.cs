using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class AbstractClassesMembersRectangle : AbstractClassesMembersShape
    {
        public override void Draw()
        {
            Console.WriteLine("Draw a rectangle");
        }
    }
}
