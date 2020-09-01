using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Models
{
  public  class Category
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }

        public Category(string name, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
        }
    }
}
