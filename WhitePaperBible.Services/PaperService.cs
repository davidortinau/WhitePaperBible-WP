using System;
using System.Collections.Generic;
using RestSharp;
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
