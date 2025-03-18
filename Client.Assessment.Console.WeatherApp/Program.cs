using Client.Assessment.Console.WeatherApp.Interface;
using Client.Assessment.Console.WeatherApp.Model;
using Client.Assessment.Console.WeatherApp.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

internal class Program
{
    /// <summary>
    /// Application is called with argumenrs which is an city or location of where the data should be build from .
    /// </summary>
    /// <param name="args">List of cities/city</param>
    /// <returns>Weather information of the cities/city</returns>
    /// <remarks>See the Launch Settings Json file for the cities/city argument</remarks>
    private static async Task Main(string[] args)
    {

        if (args is null || args.Length is 0 )
        {
            Console.WriteLine("Please provide a city name as an argument or multiple cities as argument.");
            return;
        }



        // Showing  SOLID principles of Dependency Invension with Injections 
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build())
                .AddSingleton<OpenWeatherServiceConfig>() // Register WeatherServiceConfig
                .AddSingleton<IWeatherService, OpenWeatherMap>() // Register Open Weather Map API
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog(Serilogger.Instance); // Add Serilog to DI
                })
                .BuildServiceProvider();

        // Get the weather service from the service provider
        var weatherService = serviceProvider.GetService<IWeatherService>();

        if (weatherService != null)
        {
            /// Loop through the arguments and get the weather information per city
            foreach (var arg in args)
            {
                var weatherData = await weatherService.GetWeatherInfo(arg);
                Console.WriteLine(weatherData);
               
            }
            
        }

        Console.ReadLine();
    }
}