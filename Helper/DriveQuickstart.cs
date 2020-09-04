using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
//using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Liftmanagement.Models;
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
    public class DriveQuickstart
    {

        List<GoogleDriveFolder> GoogleDriveFolders = new List<GoogleDriveFolder>();

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        // static string[] Scopes = { DriveService.Scope.DriveReadonly , DirectoryService.Scope.AdminDirectoryOrgunit, 
        //     DirectoryService.Scope.AdminDirectoryUser ,DirectoryService.Scope.AdminDirectoryDomain};
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
       
        static string ApplicationName = "Drive API .NET Quickstart";
        static Dictionary<string, Google.Apis.Drive.v3.Data.File> files = new Dictionary<string, Google.Apis.Drive.v3.Data.File>();
        private DriveService service;

        public DriveQuickstart()
        {
            UserCredential credential;

            using (var stream =
                new System.IO.FileStream("credentialsDrive.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "dominicphilomena@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
             service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
                    

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name, kind,parents,webContentLink, webViewLink)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (Google.Apis.Drive.v3.Data.File file1 in files)
                {

                    // var absPath = AbsPath(file1);
                    //  Console.WriteLine("{0} ({1} {2})", file1.Name, file1.Id,absPath);
                    Console.WriteLine("{0} ({1})", file1.Name, file1.Id);


                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            /*
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "MeinOrdner",
                MimeType = "application/vnd.google-apps.folder"
            };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            Console.WriteLine("Folder ID: " + file.Id);

            Console.Read();
            */
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
