using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WhitePaperBible.Data;
using WhitePaperBible.Mobile.Infrastructure;

namespace WhitePaperBible.Mobile
{
    public partial class MainPage : PhoneApplicationPage
    {
        private PhoneApplicationService appService = PhoneApplicationService.Current;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.ViewModel.PropertyChanged += ItemModel_PropertyChanged;

            MainListBox.SelectedIndex = -1;
        }

        private void ItemModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDataLoaded")
            {
                if (App.ViewModel.IsDataLoaded)
                {
                    loadingBar.Visibility = Visibility.Collapsed;
                    loadingBar.IsIndeterminate = false;
                    this.MainListBox.SelectionChanged += MainListBox_SelectionChanged;
                }
            }
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainListBox.SelectedIndex == -1)
                return;

            //object item = MainListBox.SelectedItem;
            //if (item == null)
            //    return;

            var paper = (Paper)MainListBox.SelectedItem;

            NavigationService.Navigate(new Uri("/PaperDetails.xaml?paperID=" + paper.id, UriKind.Relative));
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Logger.Log("");
            if (SearchText.Text == string.Empty)
                SearchText.Text = PapersViewModel.SEARCH_WATERMARK;

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            MainListBox.SelectedIndex = -1;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (appService.State.ContainsKey("searchWords"))
                SearchText.Text = appService.State["searchWords"].ToString();

            MainListBox.SelectedIndex = -1;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //appService.State["searchWords"] = SearchText.Text;

            MainListBox.SelectedIndex = -1;

            base.OnNavigatedFrom(e);
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Logger.Log("Text: " + SearchText.Text);
            if (SearchText.Text != PapersViewModel.SEARCH_WATERMARK)
                App.ViewModel.SearchPapers(SearchText.Text);
        }

        private void SearchText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text == string.Empty)
            {
                SearchText.Text = PapersViewModel.SEARCH_WATERMARK;
            }
        }

        private void SearchText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text == PapersViewModel.SEARCH_WATERMARK)
            {
                SearchText.Text = string.Empty;
            }
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Logger.Log("index: " + PanoramaControl.SelectedIndex.ToString());

            SearchText.Visibility = (PanoramaControl.SelectedIndex == 0)
                                        ? Visibility.Visible
                                        : Visibility.Collapsed;

        }
    }
}