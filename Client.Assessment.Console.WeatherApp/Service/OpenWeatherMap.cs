using Client.Assessment.Console.WeatherApp.Interface;
using Client.Assessment.Console.WeatherApp.Model;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System.Text;
using System.Text.Json;

namespace Client.Assessment.Console.WeatherApp.Service
{
    /// <summary>
    /// Utilising Open Weather MAP API to get the weather information
    /// </summary>
    public class OpenWeatherMap : IWeatherService
    {
        private readonly OpenWeatherServiceConfig weatherServiceConfig;
        public  HttpClient httpClient;
        private readonly ILogger<OpenWeatherServiceConfig> logger;
        public OpenWeatherMap(OpenWeatherServiceConfig _weatherServiceConfig ,ILogger<OpenWeatherServiceConfig> _logger)
        {
            weatherServiceConfig = _weatherServiceConfig;
            logger = _logger;
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Get the weather information from the API using Open Weater API
        /// </summary>
        /// <param name="city">City name to get weather from </param>
        /// <returns>Build up string with all weather informatyion</returns>
        public async Task<string> GetWeatherInfo(string city)
        {
            try
            {
                // Build up the request URL
                string requestUrl = $"{weatherServiceConfig.BaseUrl}?q={city}&appid={weatherServiceConfig.ApiKey}&units={weatherServiceConfig.Metric}";
                // Get results from API 
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                
                // Ensure successful response
                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the response to a model response class.

                var weatherData = JsonSerializer.Deserialize<OpenWeatherResponse>(responseBody);

                // Build up the response string into a StringBuilder rather than string to avoid mutable 
                if (weatherData != null)
                {
                    StringBuilder results = new StringBuilder();
                    results.AppendLine($"-------- Weather for {weatherData.CityName}, {weatherData.Sys.Country}   ------ ");
                    results.AppendLine($"Temperature: {weatherData.Main.Temperature}°C");
                    results.AppendLine($"Feels like: {weatherData.Main.FeelsLike}°C");
                    results.AppendLine($"Humidity: {weatherData.Main.Humidity}%");
                    results.AppendLine($"Pressure: {weatherData.Main.Pressure}hPa");
                    results.AppendLine($"Wind speed: {weatherData.Wind.Speed} m/s");
                    results.AppendLine($"Wind direction: {weatherData.Wind.Degree}°");
                    results.AppendLine("------------------------------");
                    return results.ToString();
                }
                else
                {
                    logger.LogError("Failed to parse the API response.");
                    return "Failed to parse the API response.";
                }

                
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("404"))
                {
                    logger.LogError($"City '{city}' not found. Please check spelling and try again.");
                    return ($"City '{city}' not found. Please check spelling and try again.");
                }
                else
                {
                    logger.LogError($"API request failed: {e.Message}");
                    return $"API request failed: {e.Message}";
                }
            }
            catch (JsonException)
            {
                logger.LogError("Failed to parse the API response.");

                return "Failed to parse the API response.";
            }
            catch (Exception e)
            {
               logger.LogError($"An error occurred: {e.Message}");
                return $"An error occurred: {e.Message}";
            }
        }
    }


}
