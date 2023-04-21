using LenkiMicroservice.EmailService;
using LenkiMicroservice.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LenkiData
{
    public class EmailLogic
    {
        private static readonly WebClient client = new WebClient();
        public SmsResponse SendSMS(string recipients, string messageBody, string sender)
        {
            return RunAsync(recipients, messageBody, sender);
        }

        static SmsResponse RunAsync(string recipients, string messageBody, string sender)
        {
            SmsResponse response = new SmsResponse();

            string baseUrl = "https://smsclone.com/api/sms/";
            string username = "FTCHOIR";
            string password = "ftchoir2018";
            string sender1 = "@@" + sender;
            string recipients1 = "@@" + recipients;
            string messageBody1 = "@@" + messageBody;
            try
            {
                string url = string.Format("{0}sendsms?username={1}&password={2}&sender={3}@@&recipient={4}@@&message={5}@@",
                    baseUrl, username, password, sender1, recipients1, messageBody1);
                string json = client.DownloadString(url);

                response.TotalSent = json.Split('|')[1];
                response.Cost = json.Split('|')[2];
            }
            catch (Exception e)
            {
                response.TotalSent = "0";
                response.Cost = "0";

                ErrorLog.LogException(e);
            }

            return response;
        }
    }
}
