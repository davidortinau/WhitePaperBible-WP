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
using System.Windows.Threading;
using RestSharp;
using RestSharp.Serializers;
using WhitePaperBible.Data;

namespace WhitePaperBible.Services
{
    public class PaperService
    {
        const string baseuri = "http://whitepaperbible.org/";

        public PaperService()
        {
        }

        public void GetPapers(Action<List<PaperNode>> success, Action<string> failure)
        {
            var client = new RestClient(baseuri);

            var request = new RestRequest("papers.json?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };

            client.ExecuteAsync<List<PaperNode>>(request, (response) =>
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

        public void GetPaperReferences(int paperId, Action<List<ReferenceNode>> success, Action<string> failure)
        {
            var client = new RestClient(baseuri);

            var request = new RestRequest("papers/" + paperId.ToString() + "/references.json?caller=wpb-iPhone", Method.GET) { RequestFormat = DataFormat.Json };

            client.ExecuteAsync<List<ReferenceNode>>(request, (response) =>
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
    }

    // read only from local data copies here. Do web calls from DataSync.
}
