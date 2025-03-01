using FlightReservationsSystem.Application.DTOs;
using FlightReservationsSystem.Application.Interfaces;
using FlightReservationSystem.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace FlightReservationsSystem.Api.Tests
{
    public class ReservationControllerTests
    {
        public ReservationControllerTests()
        {
            _mockReservationService = new Mock<IReservationService>();
            _controller = new ReservationsController(_mockReservationService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "sampleUserId")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var flightId = Guid.NewGuid();
            var reservation = new ReservationDto { Id = Guid.NewGuid(), FlightId = flightId, UserId = "sampleUserId" };
            _mockReservationService.Setup(s => s.CreateReservationAsync(flightId, "sampleUserId")).ReturnsAsync(reservation);

            // Act
            var result = await _controller.Create(flightId);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ReservationDto>(actionResult.Value);
            Assert.Equal(reservation.Id, returnValue.Id);
        }

        [Fact]
        public async Task GetUserReservations_ReturnsOkObjectResult()
        {
            // Arrange
            var reservations = new List<ReservationDto> { new ReservationDto { Id = Guid.NewGuid(), UserId = "sampleUserId" } };
            _mockReservationService.Setup(s => s.GetUserReservationsAsync("sampleUserId")).ReturnsAsync(reservations);

            // Act
            var result = await _controller.GetUserReservations();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ReservationDto>>(actionResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Cancel_ReturnsOkResult()
        {
            // Arrange
            var reservationId = Guid.NewGuid();
            _mockReservationService.Setup(s => s.CancelReservationAsync(reservationId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Cancel(reservationId);

            // Assert
            Assert.IsType<OkResult>(result);
        }


        private readonly Mock<IReservationService> _mockReservationService;
        private readonly ReservationsController _controller;
    }
}
