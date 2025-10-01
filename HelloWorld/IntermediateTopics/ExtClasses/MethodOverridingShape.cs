using HelloWorld.IntermediateTopics.ExtEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class MethodOverridingShape
    {
        public int Width { get; set; }
        public int Height { get; set; }
      //  public int Position Position { get; set; }
      public MethodOverridingEnumShapeType Type { get; set; }

    public virtual void Draw()
    {

    }
    }

}
