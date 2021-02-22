using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.App;
using Sample.Api.Model;

namespace Sample.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AirportController : ControllerBase
	{
		/// Will find closest airports route for every city pair
		[HttpGet("FindNearestAirport/{cities}")]
		public IActionResult FindNearest(string cities)
		{
			string host = "https://homework.appulate.com/api";
			var s = new AirportService(host);
			string[] arr = cities.Split(",");

			try {
				var list = s.GetAirports(arr).Result;
				if (list.Count == 0) {
					return Ok("There are no airports found for cities");
				} else {
					List<AirportPair> pairs = s.CalculateDistance(list);
					List<AirportPair> shortest = new List<AirportPair>();
					foreach (AirportPair ap in pairs) {
						var p = shortest.Where(a => a.IsSameRoute(ap)).SingleOrDefault();
						if (p == null) {
							shortest.Add(ap);
						} else if (ap.Distance < p.Distance) {
							shortest[shortest.FindIndex(a => a.IsSameRoute(p))] = ap;
						}
					}

					if (shortest.Count == 0) {
						return Ok("Can't build pair of cities");
					} else
						return Ok(shortest);
				}
			} catch (Exception ex) {
				return StatusCode(500, ex.Message);
			} finally {
				s.Dispose();
			}
		}
	}
}
