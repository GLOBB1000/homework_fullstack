using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Rest
{
    public class HttpRestHandler : IHttpRestHandler
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 8888;
        
        private readonly HttpClient _httpClient;

        public HttpRestHandler()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<string> Load(string version)
        {
            var mess = await _httpClient.GetAsync($"http://{IP}:{PORT}/load?version={version}");

            Debug.Log($"{mess.Content.ReadAsStringAsync().Result} : {mess.StatusCode}");
            
            if (mess.StatusCode == HttpStatusCode.NoContent)
                return "-1";
            
            var json = mess.Content.ReadAsStringAsync().Result;

            return json;
        }

        public async Task Save(string version, string json)
        {
            var mess = await _httpClient.PutAsync($"http://{IP}:{PORT}/save?version={version}",
                new StringContent(json));
            
            Debug.Log($"{mess.Content.ReadAsStringAsync().Result} : {mess.StatusCode}");
        }
    }
}