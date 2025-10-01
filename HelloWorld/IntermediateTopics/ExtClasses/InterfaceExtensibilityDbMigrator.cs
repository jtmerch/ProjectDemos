using HelloWorld.IntermediateTopics.ExtInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
   public class InterfaceExtensibilityDbMigrator
    {
        private readonly InterfaceExtensibilityILogger _logger;
        public InterfaceExtensibilityDbMigrator(InterfaceExtensibilityILogger logger)
        {
            this._logger = logger;
        }

        public void Migrate()
        {
            this._logger.LogInfo("Migration started at " + DateTime.Now);

            //code to migrate the DB

            this._logger.LogInfo("migration finished at " + DateTime.Now);
        }
    }
}
