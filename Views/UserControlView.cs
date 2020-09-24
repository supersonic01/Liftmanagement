using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Liftmanagement.Models;

namespace Liftmanagement.Views
{
   public class UserControlView : UserControl
    {
      
        public List<string> NotVisibleColumns { get; set; } = new List<string>();

        protected virtual string ViewModelName { get; }
        protected virtual string SourceObjectStringName { get; }



        protected virtual void BindingControll(ItemsControl control, string source)
        {
            Binding binding = new Binding(ViewModelName + "." + source)
            {
                Source = this
            };

            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        protected T GetSelectedObject<T>(object sender)
        {
            T model = default(T);
            var dg = sender as DataGrid;
            if (dg != null)
            {
                if (dg.SelectedItem is T)
                {
                    model = (T)dg.SelectedItem;
                }
            }

            return model;
        }

        protected virtual void BindingItem(Control control, DependencyProperty dp, string path, string stringFormat=null)
        {
            var binding = new Binding(ViewModelName + "."+ path)
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;

            if (stringFormat != null)
            {
                binding.StringFormat = stringFormat;
            }
            control.SetBinding(dp, binding);
        }


        protected virtual void BindingText(Control control,  string path)
        {
            BindingItem(control, TextBox.TextProperty, string.Format("{0}.{1}",SourceObjectStringName,path));
        }

        protected virtual void BindingComboBoxSelectedItem(Control control, string path)
        {
            BindingItem(control, ComboBox.SelectedItemProperty, string.Format("{0}.{1}", SourceObjectStringName, path));
        }

        protected virtual void BindingCheckBox(Control control, string path)
        {
            BindingItem(control, CheckBox.IsCheckedProperty, string.Format("{0}.{1}", SourceObjectStringName, path));
        }

        protected virtual void BindingDatePicker(Control control, string path)
        {
            BindingItem(control, DatePicker.SelectedDateProperty, string.Format("{0}.{1}", SourceObjectStringName, path), "dd.MM.yyyy");
        }

        protected virtual void BindingComboBox(Control control, string path)
        {
            BindingItem(control, ComboBox.ItemsSourceProperty, path);
        }

       
    }
}
