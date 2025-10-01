using HelloWorld.IntermediateTopics.ExtInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
   public class InterfaceExtensibilityFileLogger : InterfaceExtensibilityILogger
    {
        private readonly string _path;

        public InterfaceExtensibilityFileLogger(string path)
        {
            this._path = path;  
        }
        public void LogError(string message)
        {
            Log(message, "ERROR");

        }
        public void LogInfo(string message)
        {
            Log(message, "INFO");
        }

        private void Log(string message, string messageType)
        {
            using (var streamWriter = new StreamWriter(this._path, true)) //will automatically call dispose when it's time to free up resources
            {
                streamWriter.WriteLine(messageType + ": "+ message);
            }
        }
    }
}
