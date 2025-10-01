using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    abstract class AbstractClassesMembersShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public abstract void Draw();

    public void Copy()
    {
        Console.WriteLine("Copy shape into clipboard");
    }
    public void Select()
    {
        Console.WriteLine("Select the shape");
    }
}
}
