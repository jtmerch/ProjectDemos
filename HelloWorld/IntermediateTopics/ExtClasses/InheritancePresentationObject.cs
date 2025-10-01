using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    public class InheritancePresentationObject
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public void Copy()
        {
            Console.WriteLine("object copied to clipboard.");
        }        
        
        public void Duplicate()
        {
            Console.WriteLine("object was duplicated.");
        }
    }
}
