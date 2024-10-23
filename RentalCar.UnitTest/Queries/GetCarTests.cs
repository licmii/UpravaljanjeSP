
using System.Net;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace RentalCar.UnitTests
{
    public class GetCarTests
    {
        [Fact]
        public async Task GetCarById_ValidId_CarReturned()
        {
            // Given (Arrange) - setup the data
            var carId = 1; // Assuming car with ID 1 exists

            // When (Act) - simulate an API call to get the car by ID
            var response = await AnonymousClient.GetAsync($"api/Car/{carId}");

            // Then (Assert) - check the response
            using var _ = new AssertionScope();
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetCarById_InvalidId_NotFound()
        {
            // Given (Arrange) - setup the data
            var carId = 9999; // Assuming car with this ID does not exist

            // When (Act) - simulate an API call to get the car by ID
            var response = await AnonymousClient.GetAsync($"api/Car/{carId}");

            // Then (Assert) - check the response
            using var _ = new AssertionScope();
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
