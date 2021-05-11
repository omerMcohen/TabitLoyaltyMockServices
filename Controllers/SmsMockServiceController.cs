using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;
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

            inforu = inforu.Replace("&", "LogicAnd");

            Inforu result;

            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(inforu));

            using (StreamReader reader = new StreamReader(memStream, Encoding.UTF8, true))
            {
                result = (Inforu)serializer.Deserialize(reader);
            }

            object objResp = null;

            var t = Task.Run(() =>
            {
                objResp = PostSmsResponse(result);
            });

            return GetResult(objResp as IRestResponse);
        }

        private static string GetResult(IRestResponse objResp)
        {
            string result = "<Result> <Status>" + "OK" + "</Status>";
            result += "<Description>" + "Message Sent" + "</Description> ";
            result += "<NumberOfRecipients>1</NumberOfRecipients> </Result>";

            return result;
        }

        private IRestResponse PostSmsResponse(Inforu item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Settings.DeliveryNotificationUrl))
                    throw new Exception();

                var formatedUrl = item.Settings.DeliveryNotificationUrl.Replace("LogicAnd", "&");

                var client = new RestClient(formatedUrl);

                var request = new RestRequest(Method.POST);

                var body = getMessageBody(item);

                request.AddParameter("text/xml", body, ParameterType.RequestBody);

                var response = client.Execute(request);

                return response;
            }
            catch (Exception ex) 
            {
                var ErrResponse = new RestResponse();

                ErrResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;

                ErrResponse.StatusDescription = "ERROR Sending Sms";

                return ErrResponse;
            }
        }

        private object getMessageBody(Inforu item)
        {
            return @"PhoneNumber=0" + item.Recipients.PhoneNumber + "&Network=0" + item.Recipients.PhoneNumber.ToString().Substring(0, 2) + "&Status=2&StatusDescription=Delivered&ProjectId=11477&CustomerMessageId=" + item.Settings.CustomerMessageID + "&CustomerParam=&id=&SenderNumber=Tabit&BillingCodeId=1&Price=0.00&SegmentsNumber=1&ActionType=&OriginalMessage=" + item.Content.Message;
        }

    }
}
