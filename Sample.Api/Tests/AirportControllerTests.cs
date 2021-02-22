using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Sample.Api.Controllers;
using Sample.Api.Model;

namespace Sample.Api.Tests
{
	[TestFixture]
	public class AirportControllerTests {
		private AirportController _controller;

		public AirportControllerTests() {
			_controller = new AirportController();
		}
		
		[Test]
		public void WillFindShortestAirportDistance() {
			IActionResult result = _controller.FindNearest("Moscow,Saint-Petersburg");

			Assert.IsInstanceOf<OkObjectResult>(result);
			var pair = ((List<AirportPair>) ((OkObjectResult) result).Value).Single();
			Assert.That(pair.First.City, Is.EqualTo("Moscow"));
			Assert.That(pair.Second.City, Is.EqualTo("Saint-Petersburg"));
		}
		
		[Test]
		public void WillReturnNoAirportPairErrorForFirstCity() {
			IActionResult result = _controller.FindNearest("test,Saint-Petersburg");

			Assert.IsInstanceOf<OkObjectResult>(result);
			Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Can't build pair of cities"));
		}

		[Test]
		public void WillReturnNoAirportErrorForFirstCity() {
			IActionResult result = _controller.FindNearest("test,sdfs");

			Assert.IsInstanceOf<OkObjectResult>(result);
			Assert.That(((OkObjectResult)result).Value, Is.EqualTo("There are no airports found for cities"));
		}

		/*[Test]
		public void WillReturnNoAirportErrorForSecondCity() {
			IActionResult result = await _controller.FindNearest("Moscow,test");

			Assert.IsInstanceOf<OkObjectResult>(result);
			Assert.That(((OkObjectResult)result).Value, Is.EqualTo("There is no airport at test"));
		}*/
	}
}
