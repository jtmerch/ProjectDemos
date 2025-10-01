using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    class clsIndexer
    {
        public void runIndexer()
        {
            var cookie = new HttpCookie();
            cookie["name"] = "Joe";
            Console.Write(cookie["name"]);
        }

    }
}
