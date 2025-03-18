namespace Client.Assessment.Console.WeatherApp.Interface
{
    /// <summary>
    /// SOLID principles of ISP - Interface Segregation Principle 
    /// </summary>
    /// <remarks> Any wheather service can implement this interface</remarks>
    interface IWeatherService
    {
        Task<string> GetWeatherInfo(string city);
    }
}
