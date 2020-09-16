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
                    "dominicphilomena@gmail.com",
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


            var ev = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = startDate;// new DateTime(2020,9 , 4, 12, 0, 0);

            EventDateTime end = new EventDateTime();
            end.DateTime = endDate; // new DateTime(2020, 9, 4, 13, 30, 0);


            ev.Start = start;
            ev.End = end;
            ev.Summary = summary; //"New Event2";
            ev.Description = description;// "Description.2..";

            var reminder = new EventReminder();
            reminder.Method = "popup";
            reminder.Minutes = 2;

            var reminderMail = new EventReminder();
            reminder.Method = "email";
            reminder.Minutes = 2;


            /* Dim reminderList As New List(Of EventReminder)
    reminderList.Add(reminder)
    Dim remindData As New [Event].RemindersData
    remindData.UseDefault = False
    remindData.Overrides = reminderList
    anotherNewEvent.Reminders = remindData*/

            var reminderList = new List<EventReminder>();
            reminderList.Add(reminder);
            reminderList.Add(reminderMail);


            var reminderData = new Event.RemindersData
            {
                UseDefault = false,
                Overrides = reminderList
            };
            ev.Reminders = reminderData;
           



            var calendarId = "primary";
            Event recurringEvent = service.Events.Insert(ev, calendarId).Execute();
            Console.WriteLine("Event created: %s\n", ev.HtmlLink);


        }
    }
}