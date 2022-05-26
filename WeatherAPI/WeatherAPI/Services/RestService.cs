using WeatherAPI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApi;

namespace WeatherAPI.Services
{
    public class RestService : IRestService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        Root Root { get; set; }
        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<Root> GetWeather(string city)
        {
            Uri uri = new Uri($"{Constants.RestUrl}?q={city}&appid=91053a8f77b5014a034a73f4a548deed&units=metric&lang=ru");
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    string content = await responseMessage.Content.ReadAsStringAsync();
                    Root = JsonSerializer.Deserialize<Root>(content, serializerOptions);
                }
                else
                {
                    Debug.WriteLine("Bad Requset");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Root;
        }
    }
}
