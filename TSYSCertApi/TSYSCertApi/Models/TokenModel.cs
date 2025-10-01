using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TSYSCertApi.Models
{
    public class TokenModel
    {
        public string grant_type { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }

      

    }
}
