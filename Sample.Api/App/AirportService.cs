using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Sample.Api.Model;

namespace Sample.Api.App
{
	public class AirportService : HttpClient
	{
		public string ApiHost;
		public AirportService(string apiHost)
		{
			ApiHost = apiHost;
		}

		public List<AirportPair> CalculateDistance(List<Airport> airports)
		{
			List<AirportPair> pairs = new List<AirportPair>();
			Queue<Airport> queue = new Queue<Airport>(airports);
			Airport currentPort;
			while (queue.TryDequeue(out currentPort)) {
				foreach (Airport airport in queue) {
					if (!currentPort.City.Equals(airport.City)) {
						var distanceHelper = new DistanceCalculator();
						pairs.Add(new AirportPair() {
							First = currentPort,
							Second = airport,
							Distance = distanceHelper.DistanceBetweenPlaces(currentPort, airport)
						});
					}
				}
			}

			return pairs;
		}

		public async Task<List<Airport>> GetAirports(string[] cities)
		{
			cities = cities.Select(a => a.ToLower()).Distinct().ToArray();
			List<Airport> ports = new List<Airport>();

			foreach (string city in cities) {
				var cityPorts = await GetAirport(city);
				ports.AddRange(cityPorts.Where(port => port.City.ToLower().Equals(city)));
			}
			return ports;
		}

//		public async Task<Airport[]> GetAirports(string[] cities)
//		{
//			string url = "Airport/search";
//			Uri requestUri = new Uri(ApiHost + $"/Airport/search?pattern={string.Join(",", cities)}");
//			try {
//				var response = await GetAsync(requestUri);
//
//				Airport[] result = null;
//				if (response.IsSuccessStatusCode) {
//					result = await response.Content.ReadAsAsync<Airport[]>();
//					return result;
//				} else {
//					return null;
//				}
//			} catch (Exception ex) {
//				System.Diagnostics.Trace.TraceError($"Something happen during web service call: {ex.Message}");
//				throw ex;
//			}
//
//			return null;
//		}

		public async Task<Airport[]> GetAirport(string city)
		{
			string url = "Airport/search";
			Uri requestUri = new Uri(ApiHost + $"/Airport/search?pattern={city}");
			try {
				var response = await GetAsync(requestUri);

				Airport[] result = null;
				if (response.IsSuccessStatusCode) {
					result = await response.Content.ReadAsAsync<Airport[]>();
					return result;
				} else {
					return null;
				}
			} catch (Exception ex) {
				System.Diagnostics.Trace.TraceError($"Something happen during web service call: {ex.Message}");
				throw ex;
			}

			return null;
		}

	}
}