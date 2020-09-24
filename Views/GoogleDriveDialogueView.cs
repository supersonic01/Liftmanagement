using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Liftmanagement.Views
{
   public class GoogleDriveDialogueView: UserControlView
    {
        //TODO GoogleDriveconnection Settings in Settings...
        //TODO Loading indicator
        protected GoogleDriveTreeView googlDriveTree;
        protected Window windowGoogleDriveTree;

        public GoogleDriveDialogueView()
        {
            Loaded += GoogleDriveDialogueView_Loaded;
            IsVisibleChanged += GoogleDriveDialogueView_IsVisibleChanged;
        }

        private void GoogleDriveDialogueView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((bool)e.NewValue))
            {
               Console.WriteLine( "Visibility");
            }
            Console.WriteLine("Visibility11");
        }

        private void GoogleDriveDialogueView_Loaded(object sender, RoutedEventArgs e)
        {
           InitGoogleDriveDialogue();
        }

        protected void InitGoogleDriveDialogue()
        {
            googlDriveTree = new GoogleDriveTreeView();
            windowGoogleDriveTree = new Window
            {
                Title = "Google Drvie",
                Content = googlDriveTree
            };

            Uri iconUri = new Uri("pack://application:,,,../Resources/Images/Icons/Marcus-Roberto-Google-Play-Google-Drive.ico", UriKind.RelativeOrAbsolute);
            windowGoogleDriveTree.Icon = BitmapFrame.Create(iconUri);

            googlDriveTree.btnSave.Click += BtnSaveGoogleDrive_Click; ;
            googlDriveTree.btnCancel.Click += BtnCancelGoogleDrive_Click;

            windowGoogleDriveTree.Closing += WindowGoogleDriveTree_Closing;
        }

        protected virtual void BtnSaveGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WindowGoogleDriveTree_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            windowGoogleDriveTree.Hide();
        }

        private void BtnCancelGoogleDrive_Click(object sender, RoutedEventArgs e)
        {
            // windowGoogleDriveTree.Close();
            windowGoogleDriveTree.Hide();
        }

        protected void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

    }
}
