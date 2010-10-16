using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RestSharp;
using WhitePaperBible.Data;

namespace WhitePaperBible.Services
{
    public class PaperService
    {
        public PaperService()
        {
        }

        public void GetPapers(Action<List<Data.Paper>> success, Action<string> failure)
        {
            const string baseuri = "http://whitepaperbible.org/";

            var client = new RestClient(baseuri);
            
            var request = new RestRequest("papers.json", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.RootElement = "paper";

            client.ExecuteAsync<List<Data.Paper>>(request, (response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    failure(response.ErrorMessage);
                }
                else
                {
                    success(response.Data);
                }
            });
        }

        private void ParsePapers_AsJson(
            object sender, DownloadStringCompletedEventArgs e)
        {
            //JsonArray ary = new JsonArray();
            var raw = e.Result;
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



            var serializer =
                new DataContractJsonSerializer(typeof(List<Paper>));

            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(products.GetType());
            var stream =
                            new MemoryStream(Encoding.UTF8.GetBytes(raw));

            var papers = serializer.ReadObject(stream) as List<Paper>;

            stream.Close();
            //return papers;
            //lstProducts.DataContext = products;
        }
    }

    // read only from local data copies here. Do web calls from DataSync.
}
