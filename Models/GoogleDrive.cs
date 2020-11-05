using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Helper;

namespace Liftmanagement.Models
{
  public class GoogleDrive: BaseDatabaseField
    {
        private string googleDriveFolderName;

        [DisplayName("Google Drive")]
        public string GoogleDriveFolderName
        {
            get { return googleDriveFolderName; }
            set { SetField(ref googleDriveFolderName, value); }
        }

        private string googleDriveLink;

        [Database(Length = "200")]
        public string GoogleDriveLink
        {
            get { return googleDriveLink; }
            set { SetField(ref googleDriveLink, value); }
        }
        
    }
}
