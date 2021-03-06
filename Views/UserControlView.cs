﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Liftmanagement.Common;
using Liftmanagement.Converters;
using Liftmanagement.Data;
using Liftmanagement.Models;
using Liftmanagement.ViewModels;
using ValidationError = Liftmanagement.Common.ValidationError;

namespace Liftmanagement.Views
{
    public class UserControlView : UserControl, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


        private List<Control> textBoxes = null;

        public List<Control> TextBoxes
        {
            get
            {
                if (textBoxes == null)
                {
                    FindTextBoxex(this, textBoxes = new List<Control>());
                }
                return textBoxes;
            }
        }

        private BitmapImage enabledBitmapImage = new BitmapImage(new Uri("pack://application:,,,../Resources/Images/Icons/Custom-Icon-Design-Flatastic-10-Edit-validated.ico", UriKind.RelativeOrAbsolute));
        public BitmapImage EnabledBitmapImage
        {
            get { return enabledBitmapImage; }
            set { SetField(ref enabledBitmapImage, value); }
        }

        public List<string> NotVisibleColumns { get; set; } = new List<string>();

        protected virtual string ViewModelName { get; }
        protected virtual string SourceObjectStringName { get; }

        private bool isReadOnly;


        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { SetField(ref isReadOnly, value); }
        }



        protected virtual void BindingControl(ItemsControl control, string source)
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
            var source = temp.Substring(temp.IndexOf(')') + 2);

            Binding binding = new Binding(source)
            {
                Source = this
            };

            control.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        protected virtual void BindingControl<T>(FrameworkElement control, DependencyProperty dp, Expression<Func<T>> action)
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
            Selector selector = null;


            if (sender is DataGrid)
            {
                selector = sender as DataGrid;
            }
            else if (sender is ComboBox)
            {
                selector = sender as ComboBox;
            }

            if (selector != null)
            {
                if (selector.SelectedItem is T)
                {
                    model = (T)selector.SelectedItem;
                }
            }

            return model;
        }

        //protected T GetSelectedObject<T>(object sender)
        //{
        //    T model = default(T);
        //    var dg = sender as DataGrid;
        //    if (dg != null)
        //    {
        //        if (dg.SelectedItem is T)
        //        {
        //            model = (T)dg.SelectedItem;
        //        }
        //    }

        //    return model;
        //}



        protected virtual void BindingItem(Control control, DependencyProperty dp, string path, string stringFormat = null)
        {
            var binding = new Binding(ViewModelName + "." + path)
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

        protected virtual void BindingText<T>(Control control, Expression<Func<T>> action, string stringFormat = null, bool validate = false, Func<ValidationRule> validationRule = null)
        {
            BindingItem1(control, TextBox.TextProperty, GetPropertyPath(action), stringFormat);

            if (validate && validationRule != null)
            {
                Binding binding = BindingOperations.GetBinding(control, TextBox.TextProperty);
                binding.ValidationRules.Clear();
                binding.ValidationRules.Add(validationRule());
                control.LostFocus += TextBoxValidationOnLostFocus;
            }
        }


        protected virtual void BindingLabel<T>(Control control, Expression<Func<T>> action)
        {
            BindingItem1(control, Label.ContentProperty, GetPropertyPath(action));
        }

        protected virtual void MultiBindingLabel<T>(Control control, Expression<Func<T>> action1, Expression<Func<T>> action2)
        {
            Binding binding1 = new Binding(GetPropertyPath(action1))
            {
                Source = this
            };

            Binding binding2 = new Binding(GetPropertyPath(action2))
            {
                Source = this
            };

            MultiBinding mb = new MultiBinding();
            mb.Bindings.Add(binding1);
            mb.Bindings.Add(binding2);
            mb.Converter = new LabelMultiValueConverter();

            control.SetBinding(Label.ContentProperty, mb);
        }

        protected virtual void BindingDatetime<T>(Control control, Expression<Func<T>> action)
        {

            var binding = new Binding(GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Converter = new Liftmanagement.Converters.DateTimeConverter(); ;

            control.SetBinding(TextBox.TextProperty, binding);

        }

        protected virtual void BindingText(Control control, string path)
        {
            BindingItem(control, TextBox.TextProperty, string.Format("{0}.{1}", SourceObjectStringName, path));
        }

        protected virtual void BindingComboBoxSelectedItem(Control control, string path)
        {
            BindingItem(control, ComboBox.SelectedItemProperty, string.Format("{0}.{1}", SourceObjectStringName, path));
        }

        protected virtual void BindingComboBoxSelectedItem<T>(Control control, Expression<Func<T>> action)
        {
            BindingItem(control, ComboBox.SelectedItemProperty, GetPropertyPath(action));
        }

        protected virtual void BindingCheckBox(Control control, string path)
        {
            BindingItem(control, CheckBox.IsCheckedProperty, string.Format("{0}.{1}", SourceObjectStringName, path));
        }

        protected virtual void BindingCheckBox<T>(Control control, Expression<Func<T>> action)
        {
            BindingItem1(control, CheckBox.IsCheckedProperty, GetPropertyPath(action));
        }

        protected virtual void BindingComboBoxText<T>(Control control, Expression<Func<T>> action)
        {
            var binding = new Binding(GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;

            control.SetBinding(ComboBox.TextProperty, binding);
        }

        protected virtual void BindingDatePicker(Control control, string path)
        {
            BindingItem(control, DatePicker.SelectedDateProperty, string.Format("{0}.{1}", SourceObjectStringName, path), "dd.MM.yyyy");
        }

        protected virtual void BindingDatePicker<T>(Control control, Expression<Func<T>> action, bool validate = false, Func<ValidationRule> validationRule = null)
        {
            // BindingItem1(control, DatePicker.SelectedDateProperty,  GetPropertyPath(action), "dd.MM.yyyy");

            string stringFormat = "dd.MM.yyyy";
            var binding = new Binding(GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            binding.StringFormat = stringFormat;
            //binding.Converter = new Liftmanagement.Converters.DateDateConverter();

            control.SetBinding(DatePicker.SelectedDateProperty, binding);
            control.SetBinding(DatePicker.TextProperty, binding);

            //if (validate && validationRule != null)
            //{
            //    Binding bindingtxt = BindingOperations.GetBinding(control, DatePicker.TextProperty);
            //    bindingtxt.ValidationRules.Clear();
            //    bindingtxt.ValidationRules.Add(validationRule());
            //    control.LostFocus += DatePicker_LostFocus; ;
            //}
        }


        private void DatePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            var db = (Control)sender;
            //Validation.ClearInvalid(db.GetBindingExpression(DatePicker.SelectedDateProperty));
            //Validation.ClearInvalid(db.GetBindingExpression(DatePicker.TextProperty));
            ((Control)sender).GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            // ((Control)sender).GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
        }

        private void Dd_LostFocus(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void BindingComboBox(Control control, string path)
        {
            BindingItem(control, ComboBox.ItemsSourceProperty, path);
        }

        protected virtual void BindingComboBoxBindingModeOneWay<T>(Control control, Expression<Func<T>> action)
        {
            var binding = new Binding(ViewModelName + "." + GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.OneWay;

            control.SetBinding(ComboBox.ItemsSourceProperty, binding);
        }

        protected virtual void BindingComboBoxSelectedValue<T>(Control control, Expression<Func<T>> action)
        {
            var binding = new Binding(ViewModelName + "." + GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;

            control.SetBinding(ComboBox.SelectedValueProperty, binding);
        }
        protected virtual void BindingHyperlink<T>(Hyperlink hyperlink, Expression<Func<T>> action, Func<IValueConverter> converter = null)
        {
            //TODO Rework

            var binding = new Binding(GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;

            if (converter != null)
                binding.Converter = converter();

            hyperlink.SetBinding(Hyperlink.NavigateUriProperty, binding);
        }

        protected virtual void BindingTextBlock<T>(TextBlock control, Expression<Func<T>> action, Func<IValueConverter> converter = null, string stringFormat = null)
        {
            var binding = new Binding(GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;
            if (converter != null)
            {
                binding.Converter = converter();
            }

            if (stringFormat != null)
            {
                binding.StringFormat = stringFormat;
            }

            control.SetBinding(TextBlock.TextProperty, binding);
        }

        protected virtual void BindingTextBlockVisibility<T>(TextBlock control, Expression<Func<T>> action, Func<IValueConverter> converter = null)
        {
            var binding = new Binding(GetPropertyPath(action))
            {
                Source = this,
            };

            binding.Mode = BindingMode.TwoWay;
            if (converter != null)
            {
                binding.Converter = converter();
            }

            control.SetBinding(TextBlock.VisibilityProperty, binding);
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

        private void FindTextBoxex(object uiElement, IList<Control> foundOnes)
        {
            if (uiElement is TextBox)
            {
                foundOnes.Add((TextBox)uiElement);
            }
            else if (uiElement is ComboBox)
            {
                foundOnes.Add((ComboBox)uiElement);
            }
            else if (uiElement is DatePicker)
            {
                foundOnes.Add((DatePicker)uiElement);
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
            IsReadOnly = enable;

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


        protected void MarkForDelete<T>(string category, BaseDatabaseField model, Func<SQLQueryResult<T>> postActionDelete, Action postActionUpdate) where T : BaseDatabaseField
        {
            if (model == null || model.Id < 0)
            {
                new NotificationWindow("Fehler!", "Es sind kein " + category + " ausgewählt um zu löschen", null, NotificationWindow.MessageType.Waring).Show();
                return;
            }


            var msg = category + ": \n'" + model.GetFullName() + "' \nlöschen?";
            AskForceToDelete(msg, category + " löschen", () =>
            {
                try
                {
                    var titel = string.Format("{1} : {0}", model.GetFullName(), category);

                    var result = postActionDelete();

                    if (result.Records > 0)
                    {
                        if (postActionUpdate != null)
                        {
                            postActionUpdate();
                        }

                        msg = category + " wurde gelöscht.";
                        new NotificationWindow(titel, msg).Show();
                    }
                    else
                    {
                        msg = category + " konnte nicht gelöscht werden.";
                        new NotificationWindow("Fehler!", msg).Show();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    msg = category + " konnte nicht gelöscht werden.";
                    new NotificationWindow("Fehler!", exception.ToString(), null, NotificationWindow.MessageType.Error)
                        .Show();
                }
            });
        }

        protected void AskForceToEdit(string currentlyUsedBy, Action forToEditing)
        {
            var msg = "Daten sind gerade von \"" + currentlyUsedBy + "\" in Benutzung.\n Möchten Sie trotzdem die Daten bearbeiten?";
            var titel = "Daten sind in Bearbeitung";
            MessageBoxResult msgBoxResult = MessageBox.Show(msg, titel, MessageBoxButton.YesNo, MessageBoxImage.Exclamation,
                MessageBoxResult.No);

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                forToEditing();
            }
        }

        protected void AskForceToDelete(string msg, string titel, Action forToEditing)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show(msg, titel, MessageBoxButton.YesNo, MessageBoxImage.Exclamation,
                MessageBoxResult.No);

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                forToEditing();
            }
        }

        public void TextBoxValidationOnLostFocus(object sender, RoutedEventArgs e)
        {
            ((Control)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        public void Txt_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TxtSelectAll(sender);
        }

        private static void TxtSelectAll(object sender)
        {
            var txtBox = sender as TextBox;
            if (txtBox != null)
            {
                txtBox.SelectAll();
            }
        }

        public void Txt_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            TxtSelectAll(sender);
        }

        public void AssignSelectAllForTextBoxes()
        {
            foreach (var txtBox in TextBoxes)
            {
                txtBox.GotKeyboardFocus += Txt_OnGotKeyboardFocus;
                txtBox.GotMouseCapture += Txt_OnGotMouseCapture;
            }
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        protected bool IsILtemSelected(string name, Func<bool> conditonNotSelected)
        {
            bool isNotValid = conditonNotSelected();

            if (isNotValid)
            {
                new NotificationWindow("Fehler!", "Es sind kein " + name + " ausgewählt um zu bearbeiten", null, NotificationWindow.MessageType.Waring).Show();
            }

            return !isNotValid;
        }

        #region Validation

        public virtual void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            ErrorContainer = (IValidationErrorContainer)DataContext;
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(Handler), true);
        }

        public virtual void OnUnload(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(Handler));
        }

        internal IValidationErrorContainer ErrorContainer = null;

        // Based on exception handler from Josh Smith blog.
        public void Handler(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ValidationErrorEventArgs args = e as System.Windows.Controls.ValidationErrorEventArgs;

            if (args.Error.RuleInError is System.Windows.Controls.ValidationRule)
            {
                if (ErrorContainer != null)
                {
                    // Tracer.LogValidation("ViewBase.Handler called for ValidationRule exception.");

                    // Only want to work with validation errors that are Exceptions because the business object has already recorded the business rule violations using IDataErrorInfo.
                    BindingExpression bindingExpression = args.Error.BindingInError as System.Windows.Data.BindingExpression;
                    Debug.Assert(bindingExpression != null);

                    string propertyName = bindingExpression.ParentBinding.Path.Path;
                    DependencyObject OriginalSource = args.OriginalSource as DependencyObject;

                    // Construct the error message.
                    string errorMessage = "";
                    ReadOnlyObservableCollection<System.Windows.Controls.ValidationError> errors = System.Windows.Controls.Validation.GetErrors(OriginalSource);
                    if (errors.Count > 0)
                    {
                        StringBuilder builder = new StringBuilder();
                        builder.Append(propertyName).Append(":");
                        System.Windows.Controls.ValidationError error = errors[errors.Count - 1];
                        {
                            if (error.Exception == null || error.Exception.InnerException == null)
                                builder.Append(error.ErrorContent.ToString());
                            else
                                builder.Append(error.Exception.InnerException.Message);
                        }
                        errorMessage = builder.ToString();
                    }

                    // Add or remove the validation error to the validation error collection.
                    Debug.Assert(args.Action == ValidationErrorEventAction.Added || args.Action == ValidationErrorEventAction.Removed);
                    StringBuilder errorID = new StringBuilder();
                    errorID.Append(args.Error.RuleInError.ToString());
                    if (args.Action == ValidationErrorEventAction.Added)
                    {
                        ErrorContainer.AddError(new ValidationError(propertyName, errorID.ToString(), errorMessage));
                    }
                    else if (args.Action == ValidationErrorEventAction.Removed)
                    {
                        ErrorContainer.RemoveError(propertyName, errorID.ToString());
                    }
                }
            }
        }



        #endregion
    }
}
