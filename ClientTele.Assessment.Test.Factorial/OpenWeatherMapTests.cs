using Client.Assessment.Console.WeatherApp.Model;
using Client.Assessment.Console.WeatherApp.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;

namespace ClientTele.Assessment.Test.OpenWeather
{
    public class OpenWeatherMapTests
    {
        private Mock<HttpMessageHandler> httpMessageHandlerMock;
        private Mock<ILogger<OpenWeatherServiceConfig>> loggerMock;
        private OpenWeatherServiceConfig config;
        private OpenWeatherMap openWeatherMap;

        /// <summary>
        /// Initialisation of the test
        /// </summary>
        public OpenWeatherMapTests()
        {
            httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            loggerMock = new Mock<ILogger<OpenWeatherServiceConfig>>();

            //Explicitly add the configuration values
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "WeatherServiceConfig:ApiKey", "83f156186085092eacf581573389df79" },
                    { "WeatherServiceConfig:BaseUrl", "http://api.openweathermap.org/data/2.5/weather" },
                    { "WeatherServiceConfig:Metric", "celsius" }
                })
                .Build();

            // Use the mocked IConfiguration in the constructor
            config = new OpenWeatherServiceConfig(configuration);

            openWeatherMap = new OpenWeatherMap(config, loggerMock.Object)
            {
                httpClient = new HttpClient(httpMessageHandlerMock.Object)
            };
        }

        [Fact]
        public async Task GetWeatherInfo_ValidCity_ReturnsWeatherInfo()
        {
            // Arrange
            var responseContent = new StringContent("{\"coord\":{\"lon\":27.817,\"lat\":-26.8136},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":296.06,\"feels_like\":295.66,\"temp_min\":296.06,\"temp_max\":296.06,\"pressure\":1016,\"humidity\":48,\"sea_level\":1016,\"grnd_level\":859},\"visibility\":10000,\"wind\":{\"speed\":2.88,\"deg\":295,\"gust\":4.66},\"clouds\":{\"all\":64},\"dt\":1742308621,\"sys\":{\"country\":\"ZA\",\"sunrise\":1742271075,\"sunset\":1742314942},\"timezone\":7200,\"id\":957487,\"name\":\"Sasolburg\",\"cod\":200}");
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = responseContent
                });

            // Act
            var result = await openWeatherMap.GetWeatherInfo("Sasolburg");

            // Assert
            Assert.Contains("Weather for Sasolburg, ZA", result);
            Assert.Contains("Temperature: 296,06°C", result);
            Assert.Contains("Feels like: 295,66°C", result);
            Assert.Contains("Humidity: 48%", result);
            Assert.Contains("Pressure: 1016hPa", result);
            Assert.Contains("Wind speed: 2,88 m/s", result);
            Assert.Contains("Wind direction: 295°", result);
        }

        [Fact]
        public async Task GetWeatherInfo_CityNotFound_ReturnsErrorMessage()
        {
            // Arrange
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            // Act
            var result = await openWeatherMap.GetWeatherInfo("InvalidCity");

            // Assert
            Assert.Equal("City 'InvalidCity' not found. Please check spelling and try again.", result);
        }

        [Fact]
        public async Task GetWeatherInfo_ApiRequestFailed_ReturnsErrorMessage()
        {
            // Arrange
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException("API request failed"));

            // Act
            var result = await openWeatherMap.GetWeatherInfo("TestCity");

            // Assert
            Assert.Equal("API request failed: API request failed", result);
        }

        [Fact]
        public async Task GetWeatherInfo_InvalidJsonResponse_ReturnsErrorMessage()
        {
            // Arrange
            var responseContent = new StringContent("Invalid JSON");
            httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = responseContent
                });

            // Act
            var result = await openWeatherMap.GetWeatherInfo("Sasolburg");

            // Assert
            Assert.Equal("Failed to parse the API response.", result);
        }
    }
}