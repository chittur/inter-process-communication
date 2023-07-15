# Overview
One way to categorize Inter-process communication is about how the objects interact cross process: as such we can categorize the techniques into *Inter-Object Communication* and *Serialized Message Passing*. This is a sample project that demonstrates both of these techniques.

# Theory
- Inter-Object Communication:
 - Allows clients to communicate transparently with objects, regardless of where those objects are running—in the same process, on the same computer, or on a different computer. This provides a single programming model for all types of objects, and for both object clients and object servers. See [here](https://learn.microsoft.com/en-us/windows/win32/com/inter-object-communication "here").
  - The underlying technique here is marshaling: It is the mechanism by which an object that is accessible to one process can be made accessible to another process by the underlying IPC technique (unless the user wants to override it). See [here](https://learn.microsoft.com/en-us/windows/win32/com/marshaling-details "here").
  - A few technologies that provide inter-object communication are: .NET Remoting, COM, Remote Procedure Call.

- Serialized Message Passing:
 - Requires the clients to serialize data into bytes before they are shared across processes.
 - Serialization is the technique of converting the state of an object into a form that can be persisted or transported. See [here](https://learn.microsoft.com/en-us/dotnet/standard/serialization/ "here").
 - A few technologies where you would use serialization for IPC are: Named pipes, Sockets, Udp/Tcp clients. 

# Design
These are the modules in this project

- **CommunicatedObject**:
A simple class to represent a weather forecast.
- **RemotingInterface**:
Defines the .NET Remoting interface.
- **RemotingServer**:
Defines the .NET Remoting server.
- **MessagePassingServer**:
Defines the message passing server.
- **Client**
Client for the remoting server as well as the message passing server.

![Module diagram](Images/ModuleDiagram.jpg)

# Environment
The project builds and runs with Visual Studio Community 2022 when the required workloads are installed.

# Testing
- Run the Remoting Server.
- Run the Message Passing Server.
- Now run the client.

It should test
 - (1) The IPC between the client and the remoting server, as well as
 - (2) The IPC between the client and the message passing server.
