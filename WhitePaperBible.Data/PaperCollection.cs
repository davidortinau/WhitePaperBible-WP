using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
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
    [DataContract]
    public class PaperCollection
    {
        [DataMember(Name = "paper")] 
        public List<Paper> Papers { get; set; }   
    }
}
