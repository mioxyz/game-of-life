using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace GameOfLife
{
    public class CouchResponse<T> {
        public int status;
        public string name;
        public List<T> docs;

        public CouchResponse() => docs = new List<T>();
    }

    public class TestDocument
    {
        string name = "some random name";
        string _id = "asdlkj";
        public TestDocument() { }
        public string getName() => name;
    }

    public interface DatabaseInterface
    {
        string getDatabaseInfo();
        CouchResponse<T> get<T>(T obj);
    }

    public class Database : DatabaseInterface {
        public const string remoteDbUrl = "http://127.0.0.1:1911/my_database";
        HttpClient client = new HttpClient();

        public Database() { }

        public string getDatabaseInfo() {
            HttpResponseMessage response = httpGet("");
            if (response.IsSuccessStatusCode) return response.Content.ReadAsStringAsync().Result;
            else return "";
        }

        //put will cover document creation and updating in CouchDB
        public CouchResponse<T> get<T>(T obj)
        {
            Type type = obj.GetType();
            HttpResponseMessage response = httpGet("/" + type.Name + "?_id=" + type.GetProperty("_id"));
            if (response.IsSuccessStatusCode) return 
                    JsonConvert.DeserializeObject<CouchResponse<T>>(response.Content.ReadAsStringAsync().Result);
            else return
                    JsonConvert.DeserializeObject<CouchResponse<T>>("{}");
        }

        private HttpResponseMessage httpGet(string url)
        {
            client.BaseAddress = new Uri(remoteDbUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            client.Dispose();
            return response;
        }
    }
}
