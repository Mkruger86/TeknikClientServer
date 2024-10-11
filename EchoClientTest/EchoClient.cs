// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Text.Json;

Console.WriteLine("TCP client");

TcpClient tcpClient = new TcpClient("127.0.0.1", 4000);
Console.WriteLine("Client is ready");

Stream ns = tcpClient.GetStream();
StreamReader sr = new StreamReader(ns);
StreamWriter sw = new StreamWriter(ns);
sw.AutoFlush = true;
bool keepStreaming = true;
while (keepStreaming)
{
Console.WriteLine("write you message here: ");
string message = Console.ReadLine();
sw.WriteLine(message);
    if (message.Equals("close", StringComparison.OrdinalIgnoreCase))
    {
        keepStreaming = false;
        ns.Close();
        tcpClient.Close();
    }
    string response = sr.ReadLine();
    Console.WriteLine("result: " + response);
}


