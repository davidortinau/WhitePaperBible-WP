using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WhitePaperBible.Data;
using WhitePaperBible.Phone.Helpers;
using WhitePaperBible.Phone.Infrastructure;

namespace WhitePaperBible.Phone
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        //public string referencesHtml;

        // Constructor
        public DetailsPage()
        {
            Logger.Log("DetailsPage - Initialized");

            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            this.Loaded += new RoutedEventHandler(DetailsPage_Loaded);
            App.ItemModel.PropertyChanged += ItemModel_PropertyChanged;
        }

        void ItemModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Logger.Log("PropertyChanged " + e.PropertyName);
            if (e.PropertyName == "ReferencesHtmlContent")
            {
               if(App.ItemModel.IsDataLoaded)
               {
                   
                   //referencesHtml = App.ItemModel.ReferencesContent;
                   
                   // webBrowser1.Source = WebBrowserHelper.WrapHtml(referencesHtml, 460);
                   //webBrowser1.NavigateToString( WebBrowserHelper.WrapHtml(referencesHtml, 460) );
                   ReferencesContent.NavigateToString(App.ItemModel.ReferencesHtmlContent);
                   loadingBar.Visibility = Visibility.Collapsed;
                   loadingBar.IsIndeterminate = false;
               }
            }
        }

        private void DetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log("Loaded");

            DataContext = App.ItemModel;

            //ReferencesContent.NavigateToString(App.ItemModel.ReferencesHtmlContent);
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Logger.Log("DetailsPage - onNavigatedTo");
            loadingBar.Visibility = Visibility.Visible;
            loadingBar.IsIndeterminate = true;

            string paperID = "";
            if (NavigationContext.QueryString.TryGetValue("paperID", out paperID))
            {
                int id = int.Parse(paperID);
                //var paper = (Paper) App.ViewModel.Items[index];

                foreach (var paper
                    in App.ViewModel.Items)
                {
                    if(paper.id == id)
                    {
                        App.ItemModel.Paper = paper;
                        break;
                    }
                }

                //App.ItemModel.Paper = paper;
                App.ItemModel.LoadData(id);
            }
        }

        private void ReferencesContent_Loaded(object sender, RoutedEventArgs e)
        {
            Logger.Log("ReferencesContent_Loaded");
            //ReferencesContent.NavigateToString(App.ItemModel.ReferencesHtmlContent);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.ItemModel.PropertyChanged -= ItemModel_PropertyChanged;
            base.OnNavigatedFrom(e);
            App.ItemModel.Reset();
            Logger.Log("onNavigatedFrom");

        }
        
    }
}