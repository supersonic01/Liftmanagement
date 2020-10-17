using Liftmanagement.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Liftmanagement.Models
{
    public class BaseDatabaseField : DisplayNameRetriever, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        

        [DatabaseAttribute(DatabaseAttribute.AUTO_INCREMENT, Updateable = false)]
        public long Id { get; set; } = -1;

        [DatabaseAttribute(DatabaseAttribute.DEFAULT_DATETIME, Updateable = false)]
        public DateTime CreatedDate { get; set; }
        [DatabaseAttribute(DatabaseAttribute.UPDATE_DATETIME, Updateable = false)]
        public DateTime ModifiedDate { get; set; }
        [DatabaseAttribute(DatabaseAttribute.UPDATE_TIMESTAMP, Updateable = false)]
        public Timestamp Timestamp { get; set; }

        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string CreatedPersonName { get; set; } = Helper.Helper.GetPersonName();
        [DatabaseAttribute(DatabaseAttribute.NOT_NULL)]
        public string ModifiedPersonName { get; set; }= Helper.Helper.GetPersonName();

        public bool ReadOnly { get; set; }
        public string UsedBy { get; set; }

        public bool Deleted { get; set; }

        public delegate string GetFullNameDelegate();

        public void ReleaseRow()
        {
            ReadOnly = false;
            UsedBy = "";
        }
    }
}
