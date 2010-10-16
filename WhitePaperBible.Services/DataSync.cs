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
//using System.Json;
using WhitePaperBible.Data;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace WhitePaperBible.Services
{
    public class DataSync
    {
        public void BeginSync()
        {
            string baseuri = "http://whitepaperbible.org/papers.json";//172.16.96.2:3000

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += ParsePapers_AsJson;

            Uri uri = new Uri(baseuri);
            wc.DownloadStringAsync(uri);
        }

        private void ParsePapers_AsJson(
            object sender, DownloadStringCompletedEventArgs e)
        {
            //JsonArray ary = new JsonArray();
            string raw = e.Result;
            /*
            JsonArray json;
            if (JsonArray.Parse(raw) as JsonArray == null)
                json = new JsonArray { JsonObject.Parse(raw) as JsonObject };
            else
                json = JsonArray.Parse(raw) as JsonArray;

            var query = from paper in json
                        select new Paper
                        {
                            title = (string)paper["paper"]["title"],
                            id = (int)paper["paper"]["id"]
                        };
            List<Paper> papers = query.ToList() as List<Paper>;

            foreach (Paper p in papers)
            {
                //this.ContentBlock.Text += p.title + Environment.NewLine;
            }
            */
            // cache data http://dotnetaddict.dotnetdevelopersjournal.com/wp7_serialization_data.htm


            //// just to put something in there
            //this.ContentBlock.Text = papers[0].title;

            //this.ContentBlock.Text = json;
            //StringReader sr = new StringReader(json);
            //JsonReader jsonReader = new JsonTextReader(sr);

            //jsonReader.Read();
            //Paper obj = JsonConvert.DeserializeObject(jsonReader.Value.ToString()) as Paper;
            //this.ContentBlock.Text = obj.title;



            DataContractJsonSerializer serializer =
                new DataContractJsonSerializer(typeof(List<Paper>));

            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(products.GetType());
            MemoryStream stream =
                            new MemoryStream(Encoding.UTF8.GetBytes(raw));

            List<Paper> papers = serializer.ReadObject(stream) as List<Paper>;

            stream.Close();
            //return papers;
            //lstProducts.DataContext = products;
        }
    }
}
