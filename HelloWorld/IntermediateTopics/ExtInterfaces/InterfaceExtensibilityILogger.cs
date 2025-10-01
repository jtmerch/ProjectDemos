using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtInterfaces
{
    public interface InterfaceExtensibilityILogger
    {
        void LogError(string message);
        void LogInfo(string message);
    }
}
