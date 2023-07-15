/******************************************************************************
 * Filename    = IService.cs
 *
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = IPC
 * 
 * Project     = RemotingInterface
 *
 * Description = Defines the remoting interface.
 *****************************************************************************/

using CommunicatedObject;

namespace RemotingInterface
{
    /// <summary>
    /// The remoting interface.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets the weather forecast.
        /// </summary>
        /// <returns>The weather forecast</returns>
        WeatherForecast GetForecast();
    }
}
