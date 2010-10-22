using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WhitePaperBible.Data;
using WhitePaperBible.Phone.Infrastructure;

namespace WhitePaperBible.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            Logger.Log("");

            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            //var paper = (Paper) MainListBox.SelectedItem;
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log("");
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Logger.Log("");
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Logger.Log("");
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Logger.Log("Text: " + SearchText.Text);
            App.ViewModel.Papers.View.Filter = new Predicate<object>(FilterBySearch);
        }

        public bool FilterBySearch(object obj)
        {
            var paper = obj as Paper;
            if (paper != null)
                if(paper.title.Contains(SearchText.Text))
                {
                    return true;
                }else
                {
                    return false;
                }
            return false;
        
        }
    }
}