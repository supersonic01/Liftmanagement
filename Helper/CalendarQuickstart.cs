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
 public   class CalendarQuickstart
    {


        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        // static string[] Scopes = { CalendarService.Scope.CalendarReadonly };

        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        public  CalendarQuickstart(DateTime startDate,DateTime endDate, string summary, string description)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentialsCalendar.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "tokenCalendar.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "admin@aufzugsberatung-karlsruhe.de",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

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


            //var ev = new Event();
            //EventDateTime start = new EventDateTime();
            //start.DateTime = startDate;// new DateTime(2020,9 , 4, 12, 0, 0);
            //start.TimeZone = "Europe/Zurich";

            //EventDateTime end = new EventDateTime();
            //end.DateTime = endDate; // new DateTime(2020, 9, 4, 13, 30, 0);


            //ev.Start = start;
            //ev.End = end;
            //ev.Summary = summary; //"New Event2";
            //ev.Description = description;// "Description.2..";
            //ev.Location = "zu hause";

            Event ev = new Event()
            {
                Summary = "Google I/O 2015",
                Location = "800 Howard St., San Francisco, CA 94103",
                Description = "A chance to hear more about Google's developer products.",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2020-10-27T09:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2020-10-27T17:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },

            };
            ev.Transparency = "transparent";

            //var reminder = new EventReminder();
            //reminder.Method = "popup";
            //reminder.Minutes = 20;

            //var reminderMail = new EventReminder();
            //reminder.Method = "email";
            //reminder.Minutes = 20;


            /* Dim reminderList As New List(Of EventReminder)
    reminderList.Add(reminder)
    Dim remindData As New [Event].RemindersData
    remindData.UseDefault = False
    remindData.Overrides = reminderList
    anotherNewEvent.Reminders = remindData*/

            //var reminderList = new List<EventReminder>();
            //reminderList.Add(reminder);
            //reminderList.Add(reminderMail);


            //var reminderData = new Event.RemindersData
            //{
            //    UseDefault = false,
            //    Overrides = reminderList
            //};
            //ev.Reminders = reminderData;

            EventReminder rem = new EventReminder();
            rem.Method = "popup";
            rem.Minutes = 15;
            Event.RemindersData rd = new Event.RemindersData();
            rd.UseDefault = false;
            IList<EventReminder> list = new List<EventReminder>();
            list.Add(rem);
            rd.Overrides = list;
            ev.Reminders = rd;




            var calendarId = "primary";
            Event recurringEvent = service.Events.Insert(ev, calendarId).Execute();
            Console.WriteLine("Event created: %s\n", ev.HtmlLink);


        }
    }
}