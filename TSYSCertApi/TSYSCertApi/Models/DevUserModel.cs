using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSYSCertApi.Models
{
    public class DevUserModel
    {
        public string UniqueID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public DateTime JoinDate { get; set; }
        public string Email { get; set; }

        public string UserHash { get; set; }


    }
}
