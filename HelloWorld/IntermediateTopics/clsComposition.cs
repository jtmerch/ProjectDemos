using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics
{
    public class clsComposition
    {
        public void runComposition()
        {
           
            var logger = new CompositionLogger();

            var dbMigrator = new CompositionDbMigrator(logger);
            var installer = new CompositionInstaller(logger);

            dbMigrator.Migrate();
            installer.Install();


        }

    }
}
