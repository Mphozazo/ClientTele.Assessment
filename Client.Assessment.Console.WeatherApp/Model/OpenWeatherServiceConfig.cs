using Microsoft.Extensions.Configuration;


namespace Client.Assessment.Console.WeatherApp.Model
{
    public class OpenWeatherServiceConfig
    {
        /// <summary>
        /// Constructor to bind the configuration - 
        /// </summary>
        /// <param name="_config"></param>
        /// <remarks>Showing SOLID priciples for Dependency Invension </remarks>
        public OpenWeatherServiceConfig(IConfiguration _config)
        {
            _config.GetSection("WeatherServiceConfig").Bind(this);
        }

        public string? ApiKey { get; set; }
        public string? BaseUrl { get; set; }
        public string? Metric { get; set; }
    }
}
