using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsDirectoryAndDirectoryInfo
    {
        public void runDirectoryAndDirectoryInfo()
        {
            Directory.CreateDirectory(@"c:\testfolder1");

            var files = Directory.GetFiles(@"c:\testfolder1", "*.sln", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            var directories = Directory.GetDirectories(@"c:\testfolder1", "*.*", SearchOption.AllDirectories);
            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
            }

            Directory.Exists("path"); //just see if a directory exists

            var directoryInfo = new DirectoryInfo("path");
            directoryInfo.GetFiles();
            directoryInfo.GetDirectories();
        }

    }
}
