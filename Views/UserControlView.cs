using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;

namespace Liftmanagement.Views
{
   public class UserControlView : UserControl , INotifyPropertyChanged
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
    

        private List<TextBox> textBoxes = null;

        public List<TextBox> TextBoxes
        {
            get
            {
                if (textBoxes == null)
                {
                    FindTextBoxex(this, textBoxes= new List<TextBox>());
                }
                return textBoxes;
            }
        }

        private  BitmapImage enabledBitmapImage = new BitmapImage(new Uri("pack://application:,,,../Resources/Images/Icons/Custom-Icon-Design-Flatastic-10-Edit-validated.ico", UriKind.RelativeOrAbsolute));
        //TODO not working
        public  BitmapImage EnabledBitmapImage
        {
            get { return enabledBitmapImage; }
            set { SetField(ref enabledBitmapImage, value); }
        }


        public List<string> NotVisibleColumns { get; set; } = new List<string>();

        protected virtual string ViewModelName { get; }
        protected virtual string SourceObjectStringName { get; }



        protected virtual void BindingControl (ItemsControl control, string source)
        {
            Binding binding = new Binding(ViewModelName + "." + source)
            {
                Source = this
            };

            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        protected virtual void BindingControl<T>(ItemsControl control, Expression<Func<T>> action)
        {
            var temp = GetFullPath<T>(action);
            var source= temp.Substring(temp.IndexOf(')') + 2);
            
            Binding binding = new Binding(source)
            {
                Source = this
            };

            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        protected virtual void BindingControl<T>(FrameworkElement control,DependencyProperty dp, Expression<Func<T>> action)
        {
            var temp = GetFullPath<T>(action);
            var source = temp.Substring(temp.IndexOf(')') + 2);

            Binding binding = new Binding(source)
            {
                Source = this
            };
             
            control.SetBinding(dp, binding);
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

        protected virtual void BindingItem1(Control control, DependencyProperty dp, string path, string stringFormat = null)
        {
            var binding = new Binding(path)
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

        protected virtual void BindingText1(Control control, string path)
        {
            BindingItem1(control, TextBox.TextProperty, path);
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


        public string GetFullPath<T>(Expression<Func<T>> action)
        {
            return action.Body.ToString();
        }

        public string GetPropertyPath<T>(Expression<Func<T>> action)
        {
            var temp = GetFullPath<T>(action);
            return temp.Substring(temp.IndexOf(')') + 2);
        }

        private void FindTextBoxex(object uiElement, IList<TextBox> foundOnes)
        {
            if (uiElement is TextBox)
            {
                foundOnes.Add((TextBox)uiElement);
            }
            else if (uiElement is Panel)
            {
                var uiElementAsCollection = (Panel)uiElement;
                foreach (var element in uiElementAsCollection.Children)
                {
                    FindTextBoxex(element, foundOnes);
                }
            }
            else if (uiElement is UserControl)
            {
                var uiElementAsUserControl = (UserControl)uiElement;
                FindTextBoxex(uiElementAsUserControl.Content, foundOnes);
            }
            else if (uiElement is ContentControl)
            {
                var uiElementAsContentControl = (ContentControl)uiElement;
                FindTextBoxex(uiElementAsContentControl.Content, foundOnes);
            }
            else if (uiElement is Decorator)
            {
                var uiElementAsBorder = (Decorator)uiElement;
                FindTextBoxex(uiElementAsBorder.Child, foundOnes);
            }
        }

        protected virtual void EnableContoles(bool enable)
        {

            foreach (var textBox in TextBoxes)
            {
                textBox.IsEnabled = enable;
            }

           

            Uri iconUri = null;
            if (enable)
            {
                iconUri = new Uri("pack://application:,,,../Resources/Images/Icons/Custom-Icon-Design-Flatastic-10-Edit-validated.ico", UriKind.RelativeOrAbsolute);
            }
            else
            {
                iconUri = new Uri("pack://application:,,,../Resources/Images/Icons/Custom-Icon-Design-Flatastic-10-Edit-not-validated.ico", UriKind.RelativeOrAbsolute);
            }
            EnabledBitmapImage = new BitmapImage(iconUri);

            //ScreenCapture = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            //    bmp.GetHbitmap(),
            //    IntPtr.Zero,
            //    System.Windows.Int32Rect.Empty,
            //    BitmapSizeOptions.FromWidthAndHeight(width, height));
        }

        
    }
}
