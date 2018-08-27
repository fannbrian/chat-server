# chat-server

A chat server writting in C# that can be accessed via Telnet.

## Getting Started

Releases are available for Windows as an .exe at:

**[Download here](https://github.com/fannbrian/chat-server/releases)**.

Once the chat-server.exe is downloaded, simply run the executable to start the server.

If you are planning on running this server through a public ip address, there must be an inbound rule configured for TCP on port 9399 for ip addresses 0.0.0.0/0

## Known Issues

If a client closes the connection without using the /quit command, the server ends up crashing with a StackOverflow exception.

## Connecting to the Server as a Client

On Windows 7/8/10, follow this guide to enable Telnet: [How to Install and Enable Telnet in Windows 10, 8.1 and Windows 7](http://www.sysprobs.com/install-and-enable-telnet-in-windows-8-use-as-telnet-client)

Once Telnet is enabled, open up the command prompt and use the following command:

```
telnet x.x.x.x 9399
```

where x.x.x.x is the server's ip address, and 9399 is the port.


For MacOS, open up Terminal and use the following command:

```
telnet x.x.x.x 9399
```

where x.x.x.x is the server's ip address, and 9399 is the port.

## Commands
```
/join <chatroom> - Adds you to the specified chatroom. Cannot be used while in a room.
/createroom <chatroom> - Creates a chatroom and adds you to it. Cannot be used while in a room.
/w <user> - Privately messages specified user (must be in same chatroom)
/setname <name> - Sets name to specified name.
/rooms - Displays all rooms currently open as well as the number of users
/users - Displays all users currently in your chatroom. Must be in a chatroom to use.
/leave - Leaves the current chatroom
/quit - Disconnects from chat server
```

## Built With

* [Microsoft Visual Studio 2017](https://visualstudio.microsoft.com/) - IDE

## Authors

* **Brian Fann** - *Initial work* - [fannbrian](https://github.com/fannbrian)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
