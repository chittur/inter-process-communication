/******************************************************************************
 * Filename    = Server.cs
 *
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = IPC
 * 
 * Project     = RemotingServer
 *
 * Description = Defines the remoting server.
 *****************************************************************************/

using CommunicatedObject;
using RemotingInterface;
using System;

namespace RemotingServer
{
    /// <summary>
    /// The remoting server.
    /// </summary>
    public class Server : MarshalByRefObject, IService
    {
        /// <inheritdoc />
        public WeatherForecast GetForecast()
        {
            return new WeatherForecast
            {
                HighAccuracy = true,
                Summary = "Sunny in the Remoting World!",
                TemperatureCelsius = 25,
                TimeOfForecast = DateTime.Now
            };
        }
    }
}
