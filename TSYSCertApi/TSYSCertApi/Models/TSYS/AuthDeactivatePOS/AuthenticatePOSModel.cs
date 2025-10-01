using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSYSCertApi.Models.TSYS.AuthDeactivatePOS
{
    public class AuthenticatePOSModel
    {

        public string RoutingAppType { get; set; }
        public string RoutingID { get; set; }
        public string POSID { get; set; }
        public string AuthenticationCode { get; set; }
        public string MerchantPostalCode { get; set; }
        public string TransactionCode { get; set; }

    }
}
