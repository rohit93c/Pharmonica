using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
    public sealed partial class Dashboard : Page
    {
        List<Characteristics> CharList = new List<Characteristics>();
        ObservableCollection<FontFamily> fonts = new ObservableCollection<FontFamily>();
        //List<SuggestedWords> suggestedWords = new List<SuggestedWords>();
        private ObservableCollection<String> suggestions;

        public ApplicationDataContainer LocalSettings { get; }

        /// <summary>
        /// Page Load event in UWP
        /// </summary>
        public Dashboard()
        {
            suggestions = new ObservableCollection<string>();
            this.InitializeComponent();

            CharList.Add(new Characteristics() { ID = 1, Name = "Characteristics-1" });
            CharList.Add(new Characteristics() { ID = 2, Name = "Characteristics-2" });
            CharList.Add(new Characteristics() { ID = 3, Name = "Characteristics-3" });
            CharList.Add(new Characteristics() { ID = 4, Name = "Characteristics-4" });
            CharList.Add(new Characteristics() { ID = 5, Name = "Characteristics-5" });
            CharList.Add(new Characteristics() { ID = 6, Name = "ABC-1" });
            CharList.Add(new Characteristics() { ID = 7, Name = "ABC-2" });
            CharList.Add(new Characteristics() { ID = 8, Name = "ABC-3" });
            CharList.Add(new Characteristics() { ID = 9, Name = "ABC-4" });
            CharList.Add(new Characteristics() { ID = 10, Name = "ABC-5" });
            CharList.Add(new Characteristics() { ID = 11, Name = "XYZ-1" });
            CharList.Add(new Characteristics() { ID = 12, Name = "XYZ-2" });
            CharList.Add(new Characteristics() { ID = 13, Name = "XYZ-3" });
            CharList.Add(new Characteristics() { ID = 14, Name = "XYZ-4" });
            CharList.Add(new Characteristics() { ID = 15, Name = "XYZ-5" }); 

            fonts.Add(new FontFamily("Arial"));
            fonts.Add(new FontFamily("Courier New"));
            fonts.Add(new FontFamily("Times New Roman"));
            fonts.Add(new FontFamily("DEF"));
            fonts.Add(new FontFamily("GHI"));
            fonts.Add(new FontFamily("JKL"));
            fonts.Add(new FontFamily("MNO"));
            fonts.Add(new FontFamily("PQR"));
            fonts.Add(new FontFamily("STU"));
            fonts.Add(new FontFamily("VWX"));
            fonts.Add(new FontFamily("YZZ"));
            fonts.Add(new FontFamily("WTF"));

            string currDate = DateTime.Today.ToString("dd/MMMM/yyyy");
            currDate = currDate.Replace("/", "");

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime currDate2 = DateTime.Now;
            TimeSpan diff = currDate2.ToUniversalTime() - origin;
            double epochTime = Math.Floor(diff.TotalSeconds); 
            txtReportName.Text = "P" + epochTime + "_" + currDate; 
        }

        /// <summary>
        /// To go back to previous window/page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        /// <summary>
        /// Handles system-level BackRequested events and page-level back button Click events
        /// </summary>
        /// <returns></returns>
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

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
            btnAddChara1.IsEnabled = true;
            btnAddChara1.Click += ShowAddChara_Click;
            btnAddChara1.CommandParameter = 1;
        }

        /// <summary>
        /// To close Characteristics Popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (addCharaPopup.IsOpen) { addCharaPopup.IsOpen = false; }
        }

        /// <summary>
        /// Opens the Add Characteristics Popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddChara_Click(object sender, RoutedEventArgs e)
        { 
            if (!addCharaPopup.IsOpen) { addCharaPopup.IsOpen = true; }
        }
        
        /// <summary>
        /// To add characteristics for extensive serach
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCharacteristics_Click(object sender, RoutedEventArgs e)
        {
            var addedChara = CharListComboxBox.SelectedValue;
            btnAddChara1.Content = addedChara + " X";
            btnAddChara1.Click += RemoveCharacteristics_Click; 
            addCharaPopup.IsOpen = false;
        }

        /// <summary>
        /// Opens the popup to verify to remove characteristics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RemoveCharacteristics_Click(object sender, RoutedEventArgs e)
        {
            addCharaPopup.IsOpen = false;
            ContentDialogResult result = await cdRemoveChara.ShowAsync();
        }

        /// <summary>
        /// Removes the chara & set button back to previos event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cdRemoveChara_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            addCharaPopup.IsOpen = false;
            btnAddChara1.Click -= RemoveCharacteristics_Click;
            btnAddChara1.Content = "Add Characteristics +";
            btnAddChara1.Click += ShowAddChara_Click;
            //addCharaPopup.IsOpen = false;
            //cdRemoveChara.Hide();
        }

        public class Characteristics
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override string ToString() { return this.Name; }
        }

        public class SuggestedWords
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override string ToString() { return this.Name; }
        }

        private void lstSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bottomSlider.IsOpen = true;
            var sel = lstSuggestions.SelectedItem as FontFamily;
            lblSeggestions.Text = sel.Source;

            //Adding items to local storage
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings; 
            localSettings.Values["shortlistedItem"] = sel.Source;
        }

        private void ClosePopupClicked2(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (bottomSlider.IsOpen) { bottomSlider.IsOpen = false; }
        }

        /// <summary>
        /// To Shortlist an item
        /// </summary>
        static int Shortlist_Click = 0;
        private void btnShortlist_Click(object sender, RoutedEventArgs e)
        {
            #region Create TextFields
            //TextBlock shortListedWord;
            //static int Shortlist_Click = 0;
            //Shortlist_Click++;
            //for (int j = 0; j <= Shortlist_Click; j++)
            //{
            //    shortListedWord = new TextBlock
            //    {
            //        Name = j.ToString(), Text="abc", Foreground=new SolidColorBrush(Colors.Black),
            //        Margin =new Thickness(60, 15, 0, 0), TextWrapping=TextWrapping.Wrap
            //    };
            //}
            //stpShrt.Children.Add(shortListedWord); 
            #endregion

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Object value = localSettings.Values["shortlistedItem"];


            //If value is not in the list then add else skip
            //foreach (ListView  item in lstShortlist.Items)
            //{
            //    if (lstShortlist.Items.Contains(value.ToString()))
            //    {

            //    }
            //    else
            //    {
                    Shortlist_Click++;
                    lstShortlist.Items.Add(new ShortlistedItems() { ID = Shortlist_Click, Name = value.ToString() });
            //    }
            //}

            //if (!lstShortlist.Items.Any(x => x.GetType() == value))
            //{
            //    Shortlist_Click++;
            //    lstShortlist.Items.Add(new ShortlistedItems() { ID = Shortlist_Click, Name = value.ToString() }); 
            //}
            bottomSlider.IsOpen = false;
        }

        public class ShortlistedItems
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public override string ToString() { return this.Name; }
        }

        #region Commented Code
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

        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    string YouTubeId = "KvKML4TTyjQ";
        //    try
        //    {
        //        YouTubeUri url = await YouTube.GetVideoUriAsync(YouTubeId, YouTubeQuality.Quality360P);
        //        MediaElt.Source = url.Uri;
        //        MediaElt.Play();
        //    }
        //    catch (Exception)
        //    {
        //        // TODO show error (video uri not found)
        //    }
        //}
        #endregion

        private void search_Trademarks(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            if (fonts != null)
            {
                lstTrademarks.ItemsSource = fonts.Where(a => a.Source.ToUpper().Contains(searchTrademarks.QueryText.ToUpper()));
            }
        }

        private void search_Suggestions(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            if (fonts != null)
            {
                lstSuggestions.ItemsSource = fonts.Where(a => a.Source.ToUpper().Contains(searchSuggestions.QueryText.ToUpper()));
            }
        }

        //private void cdRemoveChara_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        //{
        //    cdRemoveChara.Hide();
        //}
    }
}
