using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Liftmanagement.Views
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public enum MessageType
        {
            Info,
            Waring,
            Error
        }
        public NotificationWindow(string titel, string msg, List<string> messageItems = null, MessageType msgType = MessageType.Info)
        {
            InitializeComponent();

            if (msgType == MessageType.Error || msgType == MessageType.Waring)
            {
                AnimationFrameOut.KeyFrames.Clear();
                bdrNotification.MouseUp += UIElement_OnMouseUp;
            }
            else
            {
                AnimationFrameOut.Completed += Timeline_OnCompleted;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth - 100;
                this.Top = corner.Y - this.ActualHeight;
            }));

            tbTitel.Text = titel;
            tbbMsg.Text = msg;

            if (messageItems != null)
            {
                foreach (var messageItem in messageItems)
                {
                    txtMsg.Inlines.Add("- " + messageItem + Environment.NewLine);
                }
            }
        }

      

        private void Timeline_OnCompleted(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}