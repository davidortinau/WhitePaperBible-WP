using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using WhitePaperBible.Data;
using WhitePaperBible.Services;


namespace WhitePaperBible.Phone
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public const string SEARCH_WATERMARK = "Search for...";

        private CollectionViewSource _papers;

        public MainViewModel()
        {
          
            this.Items = new ObservableCollection<Paper>();
            //this.Papers = new ObservableCollection<Paper>();
            //this.PapersCVS = new CollectionViewSource();
        }

        public string SearchText
        {
            get
            {
                if(string.Empty == _searchText)
                {
                    _searchText = SEARCH_WATERMARK;
                }
                return _searchText;
            }
            set { _searchText = value; }
        }

        /// <summary>
        /// A collection for Paper objects.
        /// </summary>
        public ObservableCollection<Paper> Items { get; private set; }
        public CollectionViewSource Papers
        {
            get
            {
                if (null == _papers)
                {
                    _papers = new CollectionViewSource();
                }
                return _papers;
            }
        }
        //public ObservableCollection<Paper> Papers { get; private set; }
        

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            var svc = new PaperService();
            svc.GetPapers(onPapersReceived, onError);

        }

        private void onError(string obj)
        {
            throw new Exception(obj);
        }

        private void onPapersReceived(List<PaperNode> papers)
        {
            foreach (var paper in papers)
            {
                //new Dispatcher().BeginInvoke(() => { this.Items.Add(paper.paper); });
                this.Items.Add(paper.paper);
            }

            this.Papers.Source = this.Items;
            //RefreshPapers();
            Papers.View.CurrentChanged += handleCurrentPaperChanged;
            this.IsDataLoaded = true;
            NotifyPropertyChanged("IsDataLoaded");
        }

        public void RefreshPapers()
        {
            Items = (ObservableCollection<Paper>)Papers.View.SourceCollection;
        }

        private void handleCurrentPaperChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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

        private string _searchText;
        public void SearchPapers(string text)
        {
            _searchText = text;
            //Papers.View.Filter = new Predicate<object>(FilterBySearch);

            this.Papers.View.Filter = r =>
            {
                if (null == r) return true;
                var rm = (Paper)r;
                var meets = rm.title.ToLowerInvariant().Contains(text.ToLowerInvariant())
                    || rm.description.ToLowerInvariant().Contains(text.ToLowerInvariant());
                return meets;
            };
        }

        public bool FilterBySearch(object obj)
        {
            var paper = obj as Paper;
            if (paper != null)
                if (paper.title.ToLower().Contains(_searchText.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            return false;

        }
    }
}