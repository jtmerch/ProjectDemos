using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class CompositionInstaller
    {

        private readonly CompositionLogger _logger;
        public CompositionInstaller(CompositionLogger logger) //constructor
        {
            this._logger = logger;
        }

        public void Install()
        {
            _logger.Log("we are installing the application.");
        }
    }
}
