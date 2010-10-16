using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WhitePaperBible.Data
{
    public class Paper
    {
        public string permalink;
        public string url_title;
        public DateTime created_at;
        public string title;
        //public bool public;
        public DateTime updated_at;
        public bool featured;
        public int id;
        public int user_id;
        public string description;
        public int view_count;

        public Paper() { }


    }
}
