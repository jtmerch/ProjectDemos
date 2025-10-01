using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsStringBuilder
    {
        public void runStringBuilder()
        {
//Stringbuilder might be better for memory management because you don't keep creating strings
//But you do not get to do string searching methods (like IndexOf), this is only for manipulation;

            var builder = new StringBuilder("Helloooo World");
            builder.Append('-', 10)
            .AppendLine()
            .Append("Header")
            .AppendLine()
            .Append('-', 10)
            .Replace('-', '+')
            .Remove(0, 10) //removes first 10 characters from string
            .Insert(0, new string('1', 10));

            Console.WriteLine( "First Char: " + builder[0]); //accesses individual characters in stringbuilder;

            Console.WriteLine(builder);

        }

    }
}
