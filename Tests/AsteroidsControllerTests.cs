using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prueba_Vecttor_Nasa.Controllers;
using Prueba_Vecttor_Nasa.Interfaces;
using Prueba_Vecttor_Nasa.Models.DTOs;
using Assert = Xunit.Assert;

#region Clase para probar el Controller de Asteroids

namespace Prueba_Vecttor_Nasa.Tests
{
	[TestClass]
	public class AsteroidsControllerTests
	{
		private readonly Mock<INasaService> _mockNasaService;
		private readonly AsteroidsController _controller;

		public AsteroidsControllerTests()
		{
			_mockNasaService = new Mock<INasaService>();
			_controller = new AsteroidsController(_mockNasaService.Object);
		}

		[TestMethod]
		public async Task Get_ReturnsBadRequest_WhenDaysIsNull()
		{
			// Act
			var result = await _controller.Get(null);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[TestMethod]
		public async Task Get_ReturnsBadRequest_WhenDaysOutOfRange()
		{
			// Act
			var resultUnder = await _controller.Get(0);
			var resultOver = await _controller.Get(8);

			// Assert
			Assert.IsType<BadRequestObjectResult>(resultUnder);
			Assert.IsType<BadRequestObjectResult>(resultOver);
		}

		[TestMethod]
		public async Task Get_ReturnsOk_WhenDaysIsValid()
		{
			// Arrange
			int validDays = 5;
			_mockNasaService.Setup(service => service.GetAsteroidsAsync(validDays))
				.ReturnsAsync(new List<AsteroidModel>());

			// Act
			var result = await _controller.Get(validDays);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[TestMethod]
		public async Task Get_ReturnsInternalServerError_WhenServiceThrowsException()
		{
			// Arrange
			int validDays = 5;
			_mockNasaService.Setup(service => service.GetAsteroidsAsync(validDays))
				.ThrowsAsync(new Exception("Service exception"));

			// Act
			var result = await _controller.Get(validDays);

			// Assert
			var objectResult = Assert.IsType<ObjectResult>(result);
			Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
		}
	}
}

#endregion