using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Sample.Api.App;
using Sample.Api.Model;

namespace Sample.Api.Tests {
	[TestFixture]
	public class AirportServiceTests {
		private AirportService _service;

		public AirportServiceTests() {
			_service = new AirportService("https://homework.appulate.com/api");
		}
		
		[Test]
		public async Task  WillFindAirport() {
			var airports = await _service.GetAirport("Moscow");

			Assert.True(airports.Length > 0);
		}
		
		[Test]
		public async Task  WillFindAirports() {
			var airports = await _service.GetAirport("Moscow,Voronezh");

			Assert.True(airports.Length > 0);
		}

		[Test]
		public async Task WillReturnNullOnIncorrectRequest() {
			var service = new AirportService("https://example.org");
			
			var airports = await _service.GetAirport("Moscow");

			Assert.AreEqual(airports, null);
		}



		/*[Test]
		public void WillReturnNoAirportErrorForSecondCity() {
			IActionResult result = await _controller.FindNearest("Moscow,test");

			Assert.IsInstanceOf<OkObjectResult>(result);
			Assert.That(((OkObjectResult)result).Value, Is.EqualTo("There is no airport at test"));
		}*/
	}
}