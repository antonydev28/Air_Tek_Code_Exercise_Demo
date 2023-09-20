using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Air_Tek_CodeExercise
{
    internal class Program
    {
        // Class to represent a Flight
        public class Flight
        {
            public int FlightNumber { get; set; }
            public string DepartureCity { get; set; }
            public string ArrivalCity { get; set; }
            public int Day { get; set; }
            public List<string> Orders { get; set; } = new List<string>(); // List of orders on this flight

            public override string ToString()
            {
                return $"Flight: {FlightNumber}, departure: {DepartureCity}, arrival: {ArrivalCity}, day: {Day}";
            }
        }

        // Load Flight Schedule method
        public static List<Flight> LoadFlightSchedule()
        {
            List<Flight> flights = new List<Flight>
        {
            new Flight { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 },
            // Add other flights here
        };
            return flights;
        }

        // Generate Flight Itineraries method
        public static void GenerateFlightItineraries(Dictionary<string, JObject> orders, List<Flight> flights)
        {
            foreach (var order in orders)
            {
                string orderId = order.Key;
                string destination = order.Value["destination"].ToString();

                // Find a suitable flight for this order
                Flight suitableFlight = flights.Find(flight => flight.ArrivalCity == destination && flight.Orders.Count < 20);

                if (suitableFlight != null)
                {
                    suitableFlight.Orders.Add(orderId);
                    Console.WriteLine($"order: {orderId}, flightNumber: {suitableFlight.FlightNumber}, " +
                                      $"departure: {suitableFlight.DepartureCity}, arrival: {suitableFlight.ArrivalCity}, day: {suitableFlight.Day}");
                }
                else
                {
                    Console.WriteLine($"order: {orderId}, flightNumber: not scheduled");
                }
            }
        }

        static void Main(string[] args)
        {
            // Load flight schedule
            List<Flight> flights = LoadFlightSchedule();

            // Load orders from JSON file
            string ordersJson = File.ReadAllText("orders.json");
            Dictionary<string, JObject> orders = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(ordersJson);

            // Generate flight itineraries
            GenerateFlightItineraries(orders, flights);

            // List out loaded flight schedule
            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }
        }
    }
}