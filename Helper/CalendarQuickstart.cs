using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Liftmanagement.Helper
{
    public class CalendarQuickstart
    {

        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API.NET Liftmanagement";
        private string gmail = "admin@aufzugsberatung-karlsruhe.de";
        private UserCredential credential;
        private string location = "Aufzugsberatung Hesse";

        public CalendarQuickstart()
        {
            using (var stream =
                new FileStream("credentialsCalendar.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "tokenCalendar.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    gmail,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
        }

        public string AddEvent(DateTime startDate, DateTime endDate, string summary, string description)
        {
            var service = GetCalendarService();

            Event ev = new Event()
            {
                Summary = summary,
                Location = location,
                Description = description,
                Start = new EventDateTime()
                {
                    DateTime = startDate
                },
                End = new EventDateTime()
                {
                    DateTime = endDate
                },
            };

            ev.Transparency = "transparent";

            EventReminder remPopup = new EventReminder();
            remPopup.Method = "popup";
            remPopup.Minutes = 15;

            EventReminder remMail = new EventReminder();
            remMail.Method = "email";
            remMail.Minutes = 15;

            Event.RemindersData rd = new Event.RemindersData();
            rd.UseDefault = false;

            IList<EventReminder> list = new List<EventReminder>();
            list.Add(remPopup);
            list.Add(remMail);

            rd.Overrides = list;
            ev.Reminders = rd;

            var calendarId = "primary";
            Event recurringEvent = service.Events.Insert(ev, calendarId).Execute();

            string eidWord = "eid=";
            var index = recurringEvent.HtmlLink.IndexOf(eidWord, 0);
            var eid = recurringEvent.HtmlLink.Substring(index + eidWord.Length, recurringEvent.HtmlLink.Length - (index + eidWord.Length));
            //var calendarEditLink = string.Format(@"https://calendar.google.com/calendar/u/2/r/eventedit/{0}?sf=true", eid);
            return eid;
        }

        public void GetCalendarItems()
        {

            var service = GetCalendarService();

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }

                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
        }

        private CalendarService GetCalendarService()
        {
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
    }
}