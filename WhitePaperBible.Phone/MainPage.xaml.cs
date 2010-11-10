using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using WhitePaperBible.Data;
using WhitePaperBible.Phone.Infrastructure;

namespace WhitePaperBible.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            Logger.Log("");

            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.ViewModel.PropertyChanged += ItemModel_PropertyChanged;
            
            MainListBox.SelectedIndex = -1;
        }

        private void ItemModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsDataLoaded")
            {
                if(App.ViewModel.IsDataLoaded)
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
            
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?paperID=" + paper.id, UriKind.Relative));

            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log("");
            //SearchText.Text = SEARCH_WATERMARK;
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            MainListBox.SelectedIndex = -1;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            MainListBox.SelectedIndex = -1;
            base.OnNavigatedTo(e);

            Logger.Log("");
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Logger.Log("");
            MainListBox.SelectedIndex = -1;
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Logger.Log("Text: " + SearchText.Text);
            if (SearchText.Text != MainViewModel.SEARCH_WATERMARK)
                App.ViewModel.SearchPapers(SearchText.Text);
        }

        private void SearchText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchText.Text == string.Empty)
            {
                App.ViewModel.SearchText = MainViewModel.SEARCH_WATERMARK;
            }
        }

        private void SearchText_GotFocus(object sender, RoutedEventArgs e)
        {
            if(SearchText.Text == MainViewModel.SEARCH_WATERMARK)
            {
                App.ViewModel.SearchText = string.Empty;
            }
        }
    }
}