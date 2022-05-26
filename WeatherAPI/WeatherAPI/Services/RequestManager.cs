using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Model;

namespace WeatherAPI.Services
{
    public class RequestManager
    {
        IRestService restService;
        public RequestManager(IRestService service)
        {
            restService = service;
        }

        public Task<Root> GetWeather(string city)
        {
            return restService.GetWeather(city);
        }
    }
}
