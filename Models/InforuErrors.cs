using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoyaltyAPI.AppCode.Helper
{
    public static class InforuErrors
    {
        static Dictionary<int, string> ShamirErros = new Dictionary<int, string>
        {
            { 1, "ok"},
            {-1, "Failed"},
            {-2, "Bad user name or password"},
            {-6, "RecipientsDataNotExists"},
            {-9, "MessageTextNotExists"},
            {-11, "IllegalXML"},
            {-13, "UserQuotaExceeded"},
            {-14, "ProjectQuotaExceeded"},
            {-15, "CustomerQuotaExceeded"},
            {-16, "WrongDateTime"},
            {-17, "WrongNumberParameter"},
            {-18, "No valid recipients"},
            {-20, "InvalidSenderNumber"},
            {-21, "InvalidSenderName,"},
            {-22, "UserBlocked"},
            {-26, "UserAuthenticationError"},
            {-28, "NetworkTypeNotSupported"},
            {-29, "NotAllNetworkTypesSupported"},
            {-90, "InvalidSenderIdentification"},
        };

        public static KeyValuePair<int, string> GetRandomShamirResult(bool isSucess)
        {
            if (isSucess)
                return ShamirErros.ElementAt(0);

            return ShamirErros.ElementAt(RandomNumber(1, 18));
        }

        // Generate a random number between two numbers  
        internal static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


    }
}