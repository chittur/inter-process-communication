/******************************************************************************
 * Filename    = Program.cs
 *
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = IPC
 * 
 * Project     = MessagePassingServer
 *
 * Description = Defines the message passing server.
 *****************************************************************************/

using CommunicatedObject;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace MessagePassingServer
{
    /// <summary>
    /// The message passing server.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Gets the weather forecast.
        /// </summary>
        /// <returns>The weather forecast</returns>
        private static WeatherForecast GetForecast()
        {
            return new WeatherForecast
            {
                HighAccuracy = true,
                Summary = "Windy in the Message Passing World!",
                TemperatureCelsius = 25,
                TimeOfForecast = DateTime.Now
            };
        }

        /// <summary>
        /// Gets the serialized weather forecast.
        /// </summary>
        /// <returns>The serialized weather forecast</returns>
        private static string GetSerializedForecast()
        {
            WeatherForecast forecast = GetForecast();
            string result = JsonSerializer.Serialize<WeatherForecast>(forecast);
            return result;
        }

        /// <summary>
        /// Runs the message passing server.
        /// </summary>
        static void RunServer()
        {
            // Set the TcpListener.
            const int port = 8011;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Buffer for reading data.
            byte[] bytes = new byte[256];

            // Enter the listening loop.
            bool processing = true;
            while (processing)
            {
                Console.WriteLine($"Waiting for a connection on Tcp port {port}.");

                // Perform a blocking call to accept requests.
                using (TcpClient client = server.AcceptTcpClient())
                {
                    Console.WriteLine("Connected!");

                    // Get a stream object for reading and writing.
                    using (NetworkStream stream = client.GetStream())
                    {
                        int i;
                        // Loop to receive all the data sent by the client.
                        do
                        {
                            i = stream.Read(bytes, 0, bytes.Length);

                            // Translate data bytes to a ASCII string.
                            string input = Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine($"Received: {input}");

                            // Quit the loop if the client sent "quit".
                            const string quit = "quit";
                            processing = !input.Equals(quit, StringComparison.OrdinalIgnoreCase);

                            // As long as the client did not send "quit", send back a response.
                            if (!string.IsNullOrEmpty(input) && processing)
                            {
                                // Send back the serialized weather forecast as the response.
                                // The client is expected to deserialize the response.
                                string reply = GetSerializedForecast();
                                byte[] output = Encoding.ASCII.GetBytes(reply);
                                stream.Write(output, 0, output.Length);
                                Console.WriteLine($"Sent: {reply}");
                            }
                        } while (i != 0);
                    }
                }
            }

            server.Stop();
        }

        /// <summary>
        /// The executive function.
        /// </summary>
        /// <param name="args">The command line args</param>
        static void Main(string[] args)
        {
            RunServer();
        }
    }
}
