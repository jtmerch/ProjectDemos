using HelloWorld.IntermediateTopics.ExtClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class CompositionDbMigrator
    {
        private readonly CompositionLogger _logger;
        public CompositionDbMigrator(CompositionLogger logger) //constructor
        {
            this._logger = logger;
        }

        public void Migrate()
        {
            _logger.Log("we are migrating dude!");
        }
    }
}
