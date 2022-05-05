using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleAPI.Controllers;
using Xunit;

namespace SimpleAPI.Tests
{
    public class WeatherForecastTest
    {
            
        [Fact]
        public void WeatherForecastGetTestMock()
        {
            //Arrange
            var loggerStub = new Mock<ILogger<WeatherForecastController>>();
            var mycontroller = new WeatherForecastController(loggerStub.Object);
            //Act
            var result = mycontroller.Get();    
            //Assert.False(result, "temperatura negativa");
            Assert.NotNull(result);
            var tempC = result.GetEnumerator().Current.TemperatureC;
            Assert.True(tempC > 0 );
            // var responseContent = result.GetEnumerator().Current.ToString();
            // Assert.NotEmpty(responseContent);
        }

        [Fact]
        public async void WeatherForecastGetTestReturn()
        {
            var webApplicationFactory = new WebApplicationFactory<SimpleAPI.Startup>();
            var client = webApplicationFactory.CreateClient();
            var result = await client.GetAsync("WeatherForecast/");
            Assert.NotNull(result);
            Assert.True(result.IsSuccessStatusCode);
            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseContent);
        }

      
    }
}
