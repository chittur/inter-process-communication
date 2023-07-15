/******************************************************************************
 * Filename    = CommunicatedObject.cs
 *
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = IPC
 * 
 * Project     = CommunicatedObject
 *
 * Description = A simple class to represent a weather forecast.
 *****************************************************************************/

using System;

namespace CommunicatedObject
{
    /// <summary>
    /// A simple class to represent a weather forecast.
    /// </summary>
    [Serializable] // Required to mark the class as serializable for remoting.
    public class WeatherForecast
    {
        public bool HighAccuracy { get; set; }
        public DateTime TimeOfForecast { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary { get; set; }
    }
}
