using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.Model;

namespace WeatherAPI.Services
{
    public interface IRestService
    {
        Task<Root> GetWeather(string city);
    }

}
