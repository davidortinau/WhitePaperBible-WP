using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using WhitePaperBible.Data;

namespace WhitePaperBible.Services
{
    public class JsonPaperDeserializer : IDeserializer
    {
        public T Deserialize<T>(RestResponse response) where T : new()
        {
            //JArray json = JArray.Parse(response.Content);
            var target = new T();
            var serializer =
                new DataContractJsonSerializer(typeof(List<Paper>));

            var stream =
                            new MemoryStream(Encoding.UTF8.GetBytes(response.Content));

            target = (T)serializer.ReadObject(stream);

            stream.Close();

            return target;
        }

        public string RootElement
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Namespace
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string DateFormat
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}