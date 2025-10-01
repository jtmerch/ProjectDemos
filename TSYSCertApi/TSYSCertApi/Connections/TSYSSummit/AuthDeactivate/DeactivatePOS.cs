using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Xml;
using TSYSCertApi.Classes;
using TSYSCertApi.Models.TSYS.AuthDeactivatePOS;

namespace TSYSCertApi.Connections.TSYSSummit.AuthDeactivate
{
    public class DeactivatePOS
    {

        public bool HasError { get; set; }
        public object ResponseObject { get; set; }



        public void ProcessRequest(DeactivatePOSModel DeactivatePOSParams, IConfiguration _configuration)
        {
            object responseObject;

            try
            {
                string routingAppType = DeactivatePOSParams.RoutingAppType;
                string routingID = DeactivatePOSParams.RoutingID;
                string POSID = DeactivatePOSParams.POSID;
                string genKey = DeactivatePOSParams.GenKey;
                string transactionCode = DeactivatePOSParams.TransactionCode;

                StringBuilder inputData = new StringBuilder();

                inputData.Append("H" + routingAppType + "." + routingID);
                inputData.Append("<SGREQ>");
                inputData.Append("<A1>" + POSID + "</A1>");
                inputData.Append("<A3>" + genKey + "</A3>");
                inputData.Append("<A10>" + transactionCode + "</A10>");
                inputData.Append("</SGREQ>");



                var XMLData = inputData.ToString();


                var ProcessTSYSSummit = new SummitConn(_configuration);
                ProcessTSYSSummit.ConnectToSummit(XMLData);

                string processorResponse = ProcessTSYSSummit.TransactionResponse;
                if (string.IsNullOrEmpty(processorResponse) == false)
                {

                    processorResponse = AppFunctions.CleanUpSummitXML(processorResponse);


                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(processorResponse);

                    XmlNode nodeBatchNumber = doc.SelectSingleNode("SGRSP/A80");
                    string BatchNumber;
                    if (nodeBatchNumber != null)
                    {
                        BatchNumber = nodeBatchNumber.InnerText.Trim();
                    }
                    else
                    {
                        BatchNumber = "0";
                    }

                    XmlNode nodeResponseCode = doc.SelectSingleNode("SGRSP/A83");
                    string ResponseCode;
                    if (nodeResponseCode != null)
                    {
                        ResponseCode = nodeResponseCode.InnerText.Trim();
                    }
                    else
                    {
                        ResponseCode = "none";
                    }

                  
                    XmlNode nodeResponseText = doc.SelectSingleNode("SGRSP/A85");
                    string ResponseText;
                    if (nodeResponseText != null)
                    {
                        ResponseText = nodeResponseText.InnerText.Trim();
                    }
                    else
                    {
                        ResponseText = "none";
                    }


                    responseObject = new
                    {
                        batchNumber = BatchNumber,
                        responseCode = ResponseCode,
                        responseText = ResponseText
                    };

                    this.HasError = false;
                }
                else
                {
                    responseObject = new
                    {
                        batchNumber = "-1",
                        responseCode = "error",
                        responseText = "Processor response is empty, please contact customer support."
                    };

                    this.HasError = true;
                }
            }

            catch (Exception ex)
            {

                responseObject = new
                {
                    batchNumber = "-1",
                    responseCode = "error",
                    responseText = $"{ex.Message} - {ex.Source}"
                };

                this.HasError = true;


            }


            this.ResponseObject = responseObject;
        }


    }
}
