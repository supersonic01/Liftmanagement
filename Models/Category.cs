using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Liftmanagement.Helper.Helper;

namespace Liftmanagement.Models
{
  public class Category
    {
        public string ImagePath { get; set; }
         public string Name { get; set; }

        public TTypeMangement MangementType { get; set; }

        public Category(string name, string imagePath, TTypeMangement mangementType)
        {
            Name = name;
            ImagePath = imagePath;
            MangementType = mangementType;
        }
    }
}
