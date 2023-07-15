/******************************************************************************
 * Filename    = Program.cs
 *
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = IPC
 * 
 * Project     = Client
 *
 * Description = Client for the remoting server as well as the message passing server.
 *****************************************************************************/

using CommunicatedObject;
using RemotingInterface;
using System;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;

namespace Client
{
    /// <summary>
    /// Client for the remoting server as well as the message passing server.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Prints the weather forecast.
        /// </summary>
        /// <param name="forecast">The weather forecast</param>
        private static void PrintWeatherForecast(WeatherForecast forecast)
        {
            Console.WriteLine($"Time: {forecast.TimeOfForecast}, Temp: {forecast.TemperatureCelsius}, Summary: {forecast.Summary}, HighAccuracy: {forecast.HighAccuracy}");
        }

        /// <summary>
        /// Gets the weather forecast via remoting.
        /// </summary>
        /// <returns>The weather forecast via remoting</returns>
        private static WeatherForecast GetWeatherForecastViaRemoting()
        {
            TcpChannel channel = new TcpChannel(8002);
            ChannelServices.RegisterChannel(channel, false);
            IService service = (IService)Activator.GetObject(typeof(IService), "tcp://localhost:8001/Server");
            WeatherForecast forecast = service.GetForecast();
            return forecast;
        }

        /// <summary>
        /// Gets the weather forecast via message passing.
        /// </summary>
        /// <returns>The weather forecast via message passing</returns>
        private static WeatherForecast GetWeatherForecastViaMessagePassing()
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer connected
            // to the same address as specified by the server, port combination.
            const int port = 8011;

            using (TcpClient client = new TcpClient("127.0.0.1", port))
            {
                string message = "Command";

                // Translate the passed message into ASCII and store it as a byte array.
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                using (NetworkStream stream = client.GetStream())
                {
                    // Send the message to the connected TcpServer.
                    stream.Write(data, 0, data.Length);

                    // Buffer to store the response bytes.
                    data = new byte[256];

                    // Receive the server response. Read the first batch of the TcpServer response bytes.
                    int bytes = stream.Read(data, 0, data.Length);
                    string response = Encoding.ASCII.GetString(data, 0, bytes);

                    WeatherForecast forecast = System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(response);
                    return forecast;
                }
            }
        }

        /// <summary>
        /// The executive function.
        /// </summary>
        /// <param name="args">The command line args</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Weather forecast via remoting:");
            PrintWeatherForecast(GetWeatherForecastViaRemoting());

            Console.WriteLine("Weather forecast via message passing:");
            PrintWeatherForecast(GetWeatherForecastViaMessagePassing());

            Console.ReadKey();
        }
    }
}
