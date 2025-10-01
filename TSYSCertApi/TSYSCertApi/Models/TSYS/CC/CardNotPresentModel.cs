using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSYSCertApi.Models.TSYS.CC
{
    public class CardNotPresentModel
    {

        public string RoutingAppType { get; set; }
        public string RoutingID { get; set; }
        public string POSID { get; set; }
        public string GenKey { get; set; }
        public string PaymentCode { get; set; }
        public string RepeatIndicator { get; set; }
        public string TransactionAmount { get; set; }
        public string SecondaryAmount { get; set; }
        public string TransOriginalNumber { get; set; }
        public string MerchantCategoryCode { get; set; }
        public string LanguageIndicator { get; set; }
        public string POSEntryData { get; set; }
        public string CardholderPresentData { get; set; }
        public string CardPresentData { get; set; }
        public string CardDataInputMode { get; set; }
        public string CardholderAuthMethod { get; set; }


    }
}
