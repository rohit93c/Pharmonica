using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pharmonica
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class index : Page
    {
        public index()
        {
            this.InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            if (!string.IsNullOrWhiteSpace(username) || Regex.IsMatch(username, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                if (!string.IsNullOrWhiteSpace(password))
                {
                    //if(username== "stark@pinstorm.com" && password== "pharma2018")
                    //{

                    //    this.Frame.Navigate(typeof(Dashboard));
                    //}
                    //else
                    //{
                    //    DialogService.ShowToast(string.Empty, "Username or password is not valid!");
                    //    return;
                    //}

                    this.Frame.Navigate(typeof(search));
                }
                else
                {
                    DialogService.ShowToast(string.Empty, "Please enter password.");
                    return;
                }
            }
            else
            {
                //throw new ApplicationException("Name is mandatory.");
                DialogService.ShowToast(string.Empty, "Please enter a valid username.");
                return;
            }
        }

        private void lnkForgotPassword_OnPointerPressed(object sender, RoutedEventArgs e)
        {

        }


        //To display dialogues in case of validation
        public class DialogService
        {
            public static void ShowToast(string message, string title)
            {
                var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                var toastElement = notificationXml.GetElementsByTagName("text");
                toastElement[0].AppendChild(notificationXml.CreateTextNode(title));
                toastElement[1].AppendChild(notificationXml.CreateTextNode(message));
                var toastNotification = new ToastNotification(notificationXml);
                ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
            }
        }

        //private void txtUsername_KeyUp(object sender, KeyRoutedEventArgs e)
        //{
        //    if ((int)e.Key == 190)
        //    {
        //        rfvUsername.Text = "This field is required";
        //    }
        //}
    }
}
