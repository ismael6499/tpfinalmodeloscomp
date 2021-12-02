using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebAppGUI.Services
{
    public class ApiGatewayClient
    {
        public static string MakePost(string path,object obj)
        {
            var urlApiGateway = Program.configuration["urls:ApiGateway"];
            string responseAsString = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApiGateway);
                var response = httpClient.PostAsJsonAsync(path, obj);
                response.Wait();
                var readTask = response.Result.Content.ReadAsStringAsync();
                readTask.Wait();
                responseAsString = readTask.Result;
            }
            return responseAsString;
        }

        public static string MakeGet(string path)
        {
            var urlApiGateway = Program.configuration["urls:ApiGateway"];
            string responseAsString = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(urlApiGateway);
                var response =  httpClient.GetAsync(path);
                response.Wait();
                var readTask = response.Result.Content.ReadAsStringAsync();
                readTask.Wait();
                responseAsString = readTask.Result;
            }
            return responseAsString;
        }
    }
}