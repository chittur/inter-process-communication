/******************************************************************************
 * Filename    = Program.cs
 *
 * Author      = Ramaswamy Krishnan-Chittur
 *
 * Product     = IPC
 * 
 * Project     = RemotingServer
 *
 * Description = The executive program for the remoting server.
 *****************************************************************************/

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp; // For the TCP channel.

namespace RemotingServer
{
    /// <summary>
    /// The executive program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The executive function.
        /// </summary>
        /// <param name="args">The command line args</param>
        static void Main(string[] args)
        {
            const int port = 8001;
            TcpChannel channel = new TcpChannel(port);       // Create a new channel.
            ChannelServices.RegisterChannel(channel, false); // Register channel.
            RemotingConfiguration.RegisterWellKnownServiceType(typeof (Server), "Server", WellKnownObjectMode.Singleton);
            Console.WriteLine($"Waiting for a connection on Tcp port {port}.");
            Console.WriteLine("Please press enter to stop the server.");
            Console.ReadLine();
        }
    }
}
