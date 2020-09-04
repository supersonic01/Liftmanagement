using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
//using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Liftmanagement.Helper
{
    public class DirectoryQuickstart
    {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        // static string[] Scopes = { DriveService.Scope.DriveReadonly , DirectoryService.Scope.AdminDirectoryOrgunit, 
        //     DirectoryService.Scope.AdminDirectoryUser ,DirectoryService.Scope.AdminDirectoryDomain};
        
        static string[] Scopes = { DirectoryService.Scope.AdminDirectoryOrgunit,
            DirectoryService.Scope.AdminDirectoryUser ,DirectoryService.Scope.AdminDirectoryDomain};
        static string ApplicationName = "Drive API .NET Quickstart";
        static Dictionary<string, Google.Apis.Drive.v3.Data.File> files = new Dictionary<string, Google.Apis.Drive.v3.Data.File>();
        private DriveService service;

        public DirectoryQuickstart()
        {
            UserCredential credential;

            using (var stream =
                new System.IO.FileStream("credentialsDirectory.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "tokenDirectory.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "dominicphilomena@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Directory API service.
            var service = new DirectoryService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.ListRequest request = service.Users.List();
            request.Customer = "my_customer";
            request.MaxResults = 10;
            request.OrderBy = UsersResource.ListRequest.OrderByEnum.Email;

            // List users.
            IList<User> users = request.Execute().UsersValue;
            Console.WriteLine("Users:");
            if (users != null && users.Count > 0)
            {
                foreach (var userItem in users)
                {
                    Console.WriteLine("{0} ({1})", userItem.PrimaryEmail,
                        userItem.Name.FullName);
                }
            }
            else
            {
                Console.WriteLine("No users found.");
            }
            Console.Read();

        }


        private  object AbsPath(Google.Apis.Drive.v3.Data.File file)
        {
            var name = file.Name;

            if (file.Parents==null || file.Parents.Count() == 0)
            {
                return name;
            }

            var path = new List<string>();

            while (true)
            {
                var parent = GetParent(file.Parents[0]);

                // Stop when we find the root dir
                if (parent.Parents == null || parent.Parents.Count() == 0)
                {
                    break;
                }

                path.Insert(0, parent.Name);
                file = parent;
            }
            path.Add(name);
            return path.Aggregate((current, next) => Path.Combine(current, next));
        }

        private  Google.Apis.Drive.v3.Data.File GetParent(string id)
        {
            // Check cache
            if (files.ContainsKey(id))
            {
                return files[id];
            }

            // Fetch file from drive
            var request = service.Files.Get(id);
            request.Fields = "name,parents";
            var parent = request.Execute();

            // Save in cache
            files[id] = parent;

            return parent;
        }


    }
}
