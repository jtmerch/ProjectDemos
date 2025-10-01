using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsFileAndFileInfo
    {
        public void runFileAndFileInfo()
        {
            /*
             * Both File and Fileinfo are very similar but File contains static methods and
             * FileInfo provides Instance methods.  So use File for a small number of operations,
             * but everytime you call static methods the OS does security checking, which can affect performance.
             * So use FileInfo and call the instance methods if you have a large number of operations.
             **/

            //File class
            string path = @"c:\somefile.jpg";
            File.Copy(@"c:\myfile.jpg", @"d:\myfilecopy.jpg", true);
            File.Delete(path);

            if (File.Exists(path))
            {
                //do something
            }

            var content = File.ReadAllText(path);

            //-----------------------------------------
            //FileInfo class
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.CopyTo("...");
            fileInfo.Delete();
            if (fileInfo.Exists)
            {
                //do something
            }

            //fileInfo.OpenRead;
        }
    }
}
