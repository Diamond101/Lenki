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

        public async Task<Response> SendEmail(string recipientEmail, string recipientName, string emailSubject, string emailBody)
        {

            var response = default(Response);

            try
            {
                string apiKey = await AppSettingLogic.GetValue("EmailAPIKey");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(await AppSettingLogic.GetValue("AppEmail"), await AppSettingLogic.GetValue("AppFriendlyName"));
                var to = new EmailAddress(recipientEmail, recipientName);


                var msg = MailHelper.CreateSingleEmail(from, to, emailSubject, null, emailBody);

                response = await client.SendEmailAsync(msg);

                return response;
            }
            catch (Exception e)
            {
                ErrorLog.LogException(e);
            }

            return response;
        }


        public async Task<string> NewUser_LoggerEmail(AllowList allow)
        {
            string emailTemplate = await AppSettingLogic.GetValue("WelcomeEmail");
            var pass = _security.Decrypt(allow.password);
            var adminname = await AppSettingLogic.GetValue("AppFriendlyName");
            var adminemail = await AppSettingLogic.GetValue("AppEmail");
            var adminphone = await AppSettingLogic.GetValue("AppPhone");

            var templateRex = new List<TemplateValue>
            {
                new TemplateValue { Value = allow.firstname, Regex = new Regex("%release.firstname%") },
                new TemplateValue { Value = allow.accessId, Regex = new Regex("%release.accessId%") },
                new TemplateValue { Value = allow.lastname, Regex = new Regex("%release.lastname%") },
                new TemplateValue { Value = allow.lastname, Regex = new Regex("%release.lastname%") },
                new TemplateValue { Value = allow.Status, Regex = new Regex("%release.status%") },
                new TemplateValue { Value = pass, Regex = new Regex("%release.pass%") },
                new TemplateValue { Value = allow.phone, Regex = new Regex("%release.phone%") },
                new TemplateValue { Value = allow.email, Regex = new Regex("%release.email%") },
                new TemplateValue { Value = allow.UserRole, Regex = new Regex("%release.userrole%") },
                new TemplateValue { Value = adminname, Regex = new Regex("%release.adminname%") },
                new TemplateValue { Value = adminemail, Regex = new Regex("%release.adminemail%") },
                new TemplateValue { Value = adminphone, Regex = new Regex("%release.adminphone%") }
            };


            foreach (var item in templateRex)
            {
                var match = item.Regex.Match(emailTemplate);

                while (match.Success)
                {
                    var value = match.Value;
                    emailTemplate = emailTemplate.Replace(value, item.Value != null ? item.Value.ToString() : string.Empty);
                    match = match.NextMatch();
                }


            }

            return emailTemplate;
        }

        public string NewPost_LoggerEmail(Post post, string recipientName)
        {
            string msg = "Hello, " + recipientName + " <br /><br />" +
                         "A new Information has been posted on the Choir Portal Details Below: <br /><br />" +
                         //"'<div class="row ticket-card mt-3 pb-2 border-bottom pb-3 mb-3"><div class="col-md-1"> <img class="img-sm rounded-circle mb-4 mb-md-0" img src="' + item.Imaage + '" alt="profile image"></div><div class="ticket-details col-md-9"><div class="d-flex"><p class="text-dark font-weight-semibold mr-2 mb-0 no-wrap">' "+
                         "<b> Post Meeting: </b>" + post.Meeting + "<br /><br />" +
                         "<b> Post Meeting: </b>" + post.Meeting + "<br /><br />" +
                         "<b>Post Date: </b>" + post.MeetingDate + "<br /><br />" +
                         "<b>Post Online: </b>" + post.Online + "<br /><br />" +
                         "<b>Post Venue: </b>" + post.Venue + "<br /><br />" +
                         "<b>Post Type: </b>" + post.Type + "<br /><br />" +
                          "<b>Post Message: </b>" + post.Message + "<br /><br />" +
                         "for further detail please visit the portal and make all necessary enquiries  thanks.<br /><br />" +
                         "------------------------------------------------------" + "<br />" +
                         "Warm Regards." + "<br />" +
                         GetConfigValue("AppFriendlyName");

            return msg;
        }

        public string NewUserSMS(AllowList allow)
        {
            var pass = _security.Decrypt(allow.password);
            string msg = "Hello, " + allow.firstname + " " +
                         "Your registration has been Completed, see details below: " +
                         "Chior ID: " + allow.accessId + "" +
                         "Surname: " + allow.firstname + "" +
                         "Last Name: " + allow.lastname + "" +
                         "Email Address: " + allow.email + "" +
                         "Phone Number: " + allow.phone + "" +
                         "Temporary Password: " + pass + "" +
                         "Chior Status: " + allow.Status + " " +
                          "Chior User Role: " + allow.UserRole + " " +
                         "You have been profile for Faith Terbanacle Choir Portal with the Above Detail do well to login and update your records thanks.<br /><br />" +
                         "------------------------------------------------------" + " " +
                         "Warm Regards." + "" +
                         GetConfigValue("AppFriendlyName");

            return msg;
        }
        
        public string NewSeatSMS(AllowList allow, SeatAllocation seat)
        {           
            string msg = "Hello, " + allow.firstname + " " +
                         "You have been Allocated a seat with below details: " +
                         "Chior ID: " + allow.accessId + "" +
                         "Surname: " + allow.firstname + "" +
                         "Last Name: " + allow.lastname + "" +
                         "Email Address: " + allow.email + "" +
                         "Phone Number: " + allow.phone + "" +
                         "Seat Number: " + seat.SeatNumber + " " +
                          "Chior User Role: " + allow.UserRole + " " +
                         "You have been Allocated a seat from Choir Portal with the Above Detail do well to locate the seat  thanks.<br /><br />" +
                         "------------------------------------------------------" + " " +
                         "Warm Regards." + "" +
                         GetConfigValue("AppFriendlyName");

            return msg;
        }
        public string NewSeat(AllowList allow, SeatAllocation seat)
        {
            string msg = "Hello, " + allow.firstname + " " +                        
                         "Seat Number: " + seat.SeatNumber + " " +
                         "You have been Allocated a seat from Choir Portal with the Above Detail do well to locate the seat  thanks.<br /><br />" +
                         "------------------------------------------------------" + " " +
                         "Warm Regards." + "" +
                         GetConfigValue("AppFriendlyName");

            return msg;
        }



        string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }

    public class TemplateValue
    {
        public string Value { get; set; }
        public Regex Regex { get; set; }
    }
}
