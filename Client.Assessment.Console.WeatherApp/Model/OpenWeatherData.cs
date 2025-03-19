using System.Text.Json.Serialization;

namespace Client.Assessment.Console.WeatherApp.Model
{
    /// <summary>
    /// Open Weather Response Model from website.
    /// </summary>
    public class OpenWeatherResponse
    {
        [JsonPropertyName("name")]
        public string CityName { get; set; } = string.Empty;

        [JsonPropertyName("weather")]
        public required WeatherInfo[] Weather { get; set; } 

        [JsonPropertyName("main")]
        public required MainInfo Main { get; set; }

        [JsonPropertyName("wind")]
        public required WindInfo Wind { get; set; }

        [JsonPropertyName("sys")]
        public required SysInfo Sys { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
    }

    public class WindInfo
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        [JsonPropertyName("deg")]
        public int Degree { get; set; }
    }

    public class SysInfo
    {
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
    }
}
