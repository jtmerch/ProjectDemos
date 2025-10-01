using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TSYSCertApi.Connections.TSYSSummit
{
    [System.Runtime.InteropServices.Guid("7E8166CC-B8D4-49EE-98E2-89FB1283C435")]
    public class SummitConn
    {

        /*****************
Hi Joe,

Good Morning Joe,
I have set up that Summit Test Account for you to use. The below is the credential information.

Platform:  Summit
Company: THOMPSON MS
Contact Name:  JOE THOMPSON
Test Phone #: 480-333-3333
Test Address: 8320 S HARDY DRIVE
Test City/State: TEMPE, AZ
Test Zip Code: 85284
Developer ID:  003352
Bin: 999995  
MID: 888000003352
Agent: 000000
Chain: 111111
Store #: 5999
Term #:  1515
Category Code: 5999
Terminal ID (V#): 75090190
POS ID: 300000450300133
Routing ID TSH950
Authentication Code: TSYS123456

Transaction URL endpoints:
POST connection: https://ssltest2h.tsysacquiring.net/scripts/gateway.dll?transact  port 15443
Socket connection: ssltest2s.tsysacquiring.net  port 15004


I have also setup a development only product so that you have an application id to use for the parse tool access.  
Please keep in mind each time you certify you will get a new application id that is for that specific offering.  
We request that you code to be able to support multiple application id’s to identify each solution.
            ******************/

        private IConfiguration _config;

        public object TransactionFields { get; set; }

        public string TransactionResponse { get; set; }

        public SummitConn(IConfiguration config)
        {
               _config = config;
        }

        public void ConnectToSummit(string XMLData)
        {
            using (HttpClient client = new HttpClient())
            {



                var requestUri = _config.GetValue<string>("ProcessorURLs:TSYSSummit:URL");
                var requestPort = _config.GetValue<string>("ProcessorURLs:TSYSSummit:Port");

                client.BaseAddress = new Uri(requestUri);

              
                var messageWrap = new ConnectionMessageWrap();
                char STX = messageWrap.STX;
                char ETX = messageWrap.ETX;
                char LRC = messageWrap.CalculateLongitudinalRedundancyCheck(XMLData + ETX);

                //create sub class to extend StringContent and override the encoding requirement
                var SubmitXML = new StringContentWithoutCharset(STX + XMLData + ETX + LRC, "x-Visa-II/x-auth");

            

                var response = client.PostAsync("", SubmitXML).Result;


                string xmlResponse = response.Content.ReadAsStringAsync().Result;

                this.TransactionResponse = xmlResponse;

               // System.Diagnostics.Debug.WriteLine($"trans result {xmlResponse}");
            }


        }

       

    }
}
