using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSYSWasm.Models
{
    public class AuthenticatedUserModel
    {
        public string token { get; set; }
        public string UserName { get; set; }
    }
}
