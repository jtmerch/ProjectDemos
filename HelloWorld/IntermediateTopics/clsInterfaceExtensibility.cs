using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    public class clsInterfaceExtensibility
    {
        public void runInterfaceExtensibility()
        {
            //   var dbMigrator = new InterfaceExtensibilityDbMigrator(new InterfaceExtensibilityConsoleLogger());
            var dbMigrator = new InterfaceExtensibilityDbMigrator(new InterfaceExtensibilityFileLogger(@"C:\log.txt"));
            dbMigrator.Migrate();
        }
    }
}
