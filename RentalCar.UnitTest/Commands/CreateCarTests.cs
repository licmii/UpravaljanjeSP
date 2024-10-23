
using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RentalCar.UnitTests
{
    public class CreateCarTests
    {
        [Fact]
        public async Task CreateCarCommand_Car_CarCreated()
        {

            var dto = new
            {
                Model = "Toyota",
                Year = 2021,
                Color = "Red"
            };
            
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await AnonymousClient.PostAsync("api/Car/Create", content);


            using var _ = new AssertionScope();
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateCarCommand_InvalidModel_BadRequest()
        {
  
            var dto = new
            {
                Year = 2021,
                Color = "Red"
            };
            
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

        
            var response = await AnonymousClient.PostAsync("api/Car/Create", content);

            using var _ = new AssertionScope();
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
