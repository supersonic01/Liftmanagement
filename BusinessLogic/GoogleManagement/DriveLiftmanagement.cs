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

namespace Liftmanagement.BusinessLogic.GoogleManagement
{
    public class DriveLiftmanagement
    {

        static string[] Scopes = { DriveService.Scope.DriveReadonly };

        static string ApplicationName = "Google Drive API.NET Liftmanagement";
        static string credentials = "credentialsDrive.json";
        static string email = "admin@aufzugsberatung-karlsruhe.de";
        string credPath = "token.json";


        //static string ApplicationName = "Drive API .NET Quickstart";
        //static string credentials = "credentialsDrive.json";
        //static string email = "dominicphilomena @gmail.com";
        //string credPath = "token.json";

        private DriveService service;

        public List<GoogleDriveFolder> GoogleDriveFolders { get; set; } = new List<GoogleDriveFolder>();

        public DriveLiftmanagement()
        {
            UserCredential credential;

            using (var stream = new System.IO.FileStream(credentials, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    email,
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
            listRequest.Fields = "nextPageToken, files(id, name, kind,parents,webContentLink, webViewLink, mimeType, shared, ownedByMe)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files.Where(c => c.MimeType == @"application/vnd.google-apps.folder" && c.OwnedByMe == true).ToList();
            //  IList<Google.Apis.Drive.v3.Data.File> files1 = listRequest.Execute().Files;

            if (files != null && files.Count > 0)
            {
                foreach (Google.Apis.Drive.v3.Data.File file1 in files)
                {

                    // var absPath = AbsPath(file1);
                    //  Console.WriteLine("{0} ({1} {2})", file1.Name, file1.Id,absPath);
                    Console.WriteLine("{0} ({1})", file1.Name, file1.Id);

                    string parentId = null;
                    if (file1.Parents != null && file1.Parents.Count() > 0)
                    {
                        var parent = GetParent(file1.Parents[0]);
                        parentId = parent.Id;
                        if (parent.Parents == null || parent.Parents.Count() == 0)
                        {
                            if (!GoogleDriveFolders.Any(c => c.Id == parent.Id))
                                GoogleDriveFolders.Add(new GoogleDriveFolder(parent.Id, parent.Name, null, parent.WebViewLink));
                        }
                    }

                    GoogleDriveFolders.Add(new GoogleDriveFolder(file1.Id, file1.Name, parentId, file1.WebViewLink));

                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }

        }

        private Google.Apis.Drive.v3.Data.File GetParent(string id)
        {
            // Fetch file from drive
            var request = service.Files.Get(id);
            request.Fields = "id,name,parents";
            var parent = request.Execute();

            return parent;
        }
    }


}
