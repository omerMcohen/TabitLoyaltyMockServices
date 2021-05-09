using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;
using LoyaltyAPI.AppCode.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;

namespace TabitLoyaltyMockServices.Controllers
{
    [RoutePrefix("api/SmsMock")]
    public class SmsMockServiceController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("send")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<Object> send([FromBody] JObject jCustomer)
        {

            var serializer = new XmlSerializer(typeof(Inforu));

            var inforu = jCustomer["inforuXML"].ToString();

            Inforu result;

            using (TextReader reader = new StringReader(inforu))
            {
                result = (Inforu)serializer.Deserialize(reader);
            }

            Task t = Task.Run(() =>
            {
                PostSmsResponse(result);
            });

            t.Wait();

            return GetResult();
        }

        private static string GetResult()
        {
            KeyValuePair<int, string> shamirResult = InforuErrors.GetRandomShamirResult(true);

            string result = "<Result> <Status>" + shamirResult.Key + "</Status>";
            result += "<Description>" + shamirResult.Value + "</Description> ";
            result += "<NumberOfRecipients>1</NumberOfRecipients> </Result>";

            return result;
        }

        private void PostSmsResponse(Inforu item)
        {
            var client = new RestClient(item.Settings.DeliveryNotificationUrl);

            var request = new RestRequest(Method.POST);

            var body = getMessageBody(item);

            request.AddParameter("text/xml", body, ParameterType.RequestBody);

            try
            {
                // execute the request
                IRestResponse response = client.Execute(request);
            }
            catch (Exception) { }
        }

        private object getMessageBody(Inforu item)
        {
            return @"PhoneNumber=0" + item.Recipients.PhoneNumber + "&Network=0" + item.Recipients.PhoneNumber.ToString().Substring(0, 2) + "&Status=2&StatusDescription=Delivered&ProjectId=11477&CustomerMessageId=" + item.Settings.CustomerMessageID + "&CustomerParam=&id=&SenderNumber=Tabit&BillingCodeId=1&Price=0.00&SegmentsNumber=1&ActionType=&OriginalMessage=" + item.Content.Message;
        }

    }
}
