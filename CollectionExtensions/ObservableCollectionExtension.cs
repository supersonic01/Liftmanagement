using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.CollectionExtensions
{
    public static class ObservableCollectionExtension
    {
        public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> list)
        {
            foreach (var i in list) collection.Add(i);

            return collection;
        }
    }
}