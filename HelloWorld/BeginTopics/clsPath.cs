using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsPath
    {
        public void runPath()
        {
            var newpath = @"c:\testdirectory\testfile.jpg";

            //to get extension
            Console.WriteLine("Extension: " + Path.GetExtension(newpath));
            Console.WriteLine("File Name: " + Path.GetFileName(newpath));
            Console.WriteLine("Get File Name without Extension: " + Path.GetFileNameWithoutExtension(newpath));
            Console.WriteLine("Directory Name: " + Path.GetDirectoryName(newpath));

        }

        }
}
