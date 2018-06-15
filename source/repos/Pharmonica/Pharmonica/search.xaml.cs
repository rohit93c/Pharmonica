using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class search : Page
    {
        List<Characteristics> CharList = new List<Characteristics>();
        ObservableCollection<FontFamily> fonts = new ObservableCollection<FontFamily>();
        public search()
        {
            suggestions = new ObservableCollection<string>(); 
            this.InitializeComponent();

            CharList.Add(new Characteristics() { ID = 1, Name = "Characteristics-1" });
            CharList.Add(new Characteristics() { ID = 2, Name = "Characteristics-2" });
            CharList.Add(new Characteristics() { ID = 3, Name = "Characteristics-3" });
            CharList.Add(new Characteristics() { ID = 4, Name = "Characteristics-4" });
            CharList.Add(new Characteristics() { ID = 5, Name = "Characteristics-5" });

            fonts.Add(new FontFamily("Arial"));
            fonts.Add(new FontFamily("Courier New"));
            fonts.Add(new FontFamily("Times New Roman"));
            fonts.Add(new FontFamily("Arial"));
            fonts.Add(new FontFamily("Courier New"));
            fonts.Add(new FontFamily("Times New Roman"));
            fonts.Add(new FontFamily("Arial"));
            fonts.Add(new FontFamily("Courier New"));
            fonts.Add(new FontFamily("Times New Roman"));
            fonts.Add(new FontFamily("Arial"));
            fonts.Add(new FontFamily("Courier New"));
            fonts.Add(new FontFamily("Times New Roman"));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Dashboard));
        }

        private ObservableCollection<String> suggestions;


        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        { 
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput) 
            { 
                suggestions.Clear(); 
                suggestions.Add(sender.Text + "1"); 
                suggestions.Add(sender.Text + "2"); 
                suggestions.Add(sender.Text + "3"); 
                suggestions.Add(sender.Text + "4"); 
                suggestions.Add(sender.Text + "5"); 
                suggestions.Add(sender.Text + "6"); 
                suggestions.Add(sender.Text + "7"); 
                suggestions.Add(sender.Text + "8"); 
                suggestions.Add(sender.Text + "9"); 
                sender.ItemsSource = suggestions;
                txtAutoSuggestBox.IsSuggestionListOpen = false;
            } 
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        { 
            if (args.ChosenSuggestion != null) 
                txtAutoSuggestBox.Text = args.ChosenSuggestion.ToString(); 
            else 
                txtAutoSuggestBox.Text = sender.Text; 
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        { 
            txtAutoSuggestBox.Text = "Choosen";
            txtAutoSuggestBox.IsSuggestionListOpen = false;
        }

        //To open Popup
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        //Opens the Popup. 
        private void ShowAddChara_Click(object sender, RoutedEventArgs e)
        {  
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }

        //private async void ShowAddChara_Click(object sender, RoutedEventArgs e)
        //{
        //    ContentDialogResult result = await AddCharacteristics.ShowAsync();
        //    if (result == ContentDialogResult.Primary)
        //    { 
        //    }
        //    else
        //    { 
        //    }
        //} 

        public class Characteristics
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override string ToString() { return this.Name; }
        }
    }
}
