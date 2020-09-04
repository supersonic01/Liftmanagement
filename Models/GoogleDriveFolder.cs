using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
   public class GoogleDriveFolder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string WebLink { get; set; }
    
        public GoogleDriveFolder(string id, string name, string parentId, string weblink)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            WebLink = weblink;
        }
    }
}
