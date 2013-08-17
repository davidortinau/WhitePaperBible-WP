using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WhitePaperBible.Data;
using WhitePaperBible.Phone.Helpers;
using WhitePaperBible.Phone.Infrastructure;
using WhitePaperBible.Services;

namespace WhitePaperBible.Phone
{
    public class PaperDetailViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Reference> Items;
        private bool _isDataLoaded;
        private string _referencesHtmlContent;
        private Paper _paper;

        public PaperDetailViewModel()
        {
            this.Items = new ObservableCollection<Reference>();
        }
        
        public Paper Paper
        {
            get { return _paper; }
            set
            {
                if(value != _paper)
                {
                    _paper = value;
                    NotifyPropertyChanged("Paper");
                }
            }
        }

        public string ReferencesHtmlContent
        {
            get { return _referencesHtmlContent; }
            set
            {
                if(value != _referencesHtmlContent)
                {
                    _referencesHtmlContent = value;
                    NotifyPropertyChanged("ReferencesHtmlContent");
                }
            }
        }

        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            private set { _isDataLoaded = value; NotifyPropertyChanged("IsDataLoaded"); }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData(int paperId)
        {
            var svc = new PaperService();
            svc.GetPaperReferences(paperId, onReferencesReceived, onError);

        }

        private void onError(string obj)
        {
            throw new Exception(obj);
        }

        private void onReferencesReceived(List<ReferenceNode> references)
        {
            var referencesContent = string.Empty;
            foreach (var reference in references)
            {
                this.Items.Add(reference.reference);
                referencesContent += reference.reference.content;
            }

            Logger.Log("MODEL onReferencesReceived");

            IsDataLoaded = true;
            ReferencesHtmlContent = WebBrowserHelper.WrapHtml(referencesContent, 450);

            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Reset()
        {
            Paper = null;
            ReferencesHtmlContent = string.Empty;
            IsDataLoaded = false;
        }
    }
}