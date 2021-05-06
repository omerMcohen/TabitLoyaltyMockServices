using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;
using LoyaltyAPI.AppCode.Helper;
using RestSharp;

namespace TabitLoyaltyMockServices.Controllers
{
    [RoutePrefix("api/SmsMock")]
    public class SmsMockServiceController : ApiController
    {
        [HttpPost]
        [Route("send")]
        public async Task<Object> send(HttpRequestMessage request)
        {
            string requestBody = await request.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(InforuRoot));
            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            var requestXml = (InforuRoot)serializer.Deserialize(memStream);
            int numberOfMessageSend = requestXml.Inforu.Length;


            // handle post result 
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                HandleRequest(requestXml);
            }).Start();

            Thread.Sleep(300); // mock delay of 300 ms
            return GetShamirSingleResult(numberOfMessageSend);
        }

        private static string GetShamirSingleResult()
        {
            KeyValuePair<int, string> shamirResult = InforuErrors.GetRandomShamirResult(true);

            string result = "<Result> <Status>" + shamirResult.Key + "</Status>";
            result += "<Description>" + shamirResult.Value + "</Description> ";
            result += "<NumberOfRecipients>1</NumberOfRecipients> </Result>";

            return result;
        }


        private void HandleRequest(InforuRoot requestXml)
        {
            foreach (var item in requestXml.Inforu)
            {
                PostSmsResponse(item);
            }
        }

        private void PostSmsResponse(InforuRootInforu item)
        {
            var client = new RestClient("https://tabitloyaltyaf-dev.azurewebsites.net/api/");

            var request = new RestRequest("IL_HttpTrigger_SmsProviderCallBack", Method.POST);

            var body = getMessageBody(item);

            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            try
            {
                // execute the request
                IRestResponse response = client.Execute(request);
            }
            catch (Exception) { }
        }

        private object getMessageBody(InforuRootInforu item)
        {
            return @"PhoneNumber=0" + item.Recipients.PhoneNumber + "&Network=0" + item.Recipients.PhoneNumber.ToString().Substring(0, 2) + "&Status=2&StatusDescription=Delivered&ProjectId=11477&CustomerMessageId=" + item.Settings.CustomerMessageID + "&CustomerParam=&id=&SenderNumber=Tabit&BillingCodeId=1&Price=0.00&SegmentsNumber=1&ActionType=&OriginalMessage=" + item.Content.Message;
        }

    }
}
