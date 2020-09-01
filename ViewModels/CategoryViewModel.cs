using Liftmanagement.Data;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.ViewModels
{
  public class CategoryViewModel: ViewModel
    {
        private List<Category> categories = new List<Category>();

        public List<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        public CategoryViewModel()
        {
            categories.Add(new Category("Kunden", @"\Resources\Images\Icons\Office-Customer-Male-Light.ico"));
            DataAccess.Test();
        }
    }
}
