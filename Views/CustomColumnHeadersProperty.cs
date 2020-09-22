﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Liftmanagement.Views
{
    public static class CustomColumnHeadersProperty
    {
        public static DependencyProperty ItemTypeProperty = DependencyProperty.RegisterAttached(
            "ItemType",
            typeof(Type),
            typeof(CustomColumnHeadersProperty),
            new PropertyMetadata(OnItemTypeChanged));

        public static void SetItemType(DependencyObject obj, Type value)
        {
            obj.SetValue(ItemTypeProperty, value);
        }

        public static Type GetItemType(DependencyObject obj)
        {
            return (Type)obj.GetValue(ItemTypeProperty);
        }

        private static void OnItemTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var dataGrid = sender as DataGrid;

            if (args.NewValue != null)
                dataGrid.AutoGeneratingColumn += dataGrid_AutoGeneratingColumn;
            else
                dataGrid.AutoGeneratingColumn -= dataGrid_AutoGeneratingColumn;
        }

        static void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            //  var viewType = GetItemType(sender as DataGrid);

            var dataGrid = (sender as DataGrid);
            var collection = dataGrid.ItemsSource;
            Type collectionType = collection.GetType();

            var listCollection = collection as ListCollectionView;
            if (listCollection != null)
            {
                collectionType = listCollection.SourceCollection.GetType();
            }

            Type itemType = collectionType.GetGenericArguments().Single();


            MemberInfo property = itemType.GetProperty(e.PropertyName);
            var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
            if (attribute == null)
            {
                e.Column.Visibility = Visibility.Collapsed;

                return;
            }
                

            var displayAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single();

            if (displayAttribute.DisplayName != null)
            {
                e.Column.Header = displayAttribute.DisplayName;              
            }

           
           if (dataGrid.Tag!= null)
            {
                List<string> notVisibleColumns = (List<string>)dataGrid.Tag;

                if(notVisibleColumns.Count > 0)
                {
                    if (notVisibleColumns.Contains(e.PropertyName))
                    {
                        e.Column.Visibility = Visibility.Collapsed;
                    }
                }
            }
           //if(dataGrid.Columns.Where(c=> c.Visibility == Visibility.Visible).Last() == e.Column)
           // {
           //     Console.WriteLine();
           //    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
           // }

        }
    }
}
