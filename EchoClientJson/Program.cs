// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Text.Json;
using EchoServer;

Console.WriteLine("TCP client");

TcpClient tcpClient = new TcpClient("127.0.0.1", 4000);
Console.WriteLine("Client is ready");

Stream ns = tcpClient.GetStream();
StreamReader sr = new StreamReader(ns);
StreamWriter sw = new StreamWriter(ns);
sw.AutoFlush = true;
InputData inputData = new InputData("", "", "");
bool keepStreaming = true;
while (keepStreaming)
{
    Console.WriteLine("type of operation? (+, -, *, / or random: ");
    inputData.Operation = Console.ReadLine();
    Console.WriteLine("write first number: ");
    inputData.Input1 = Console.ReadLine();
    Console.WriteLine("write second number: ");
    inputData.Input2 = Console.ReadLine();
    var jsonMessage = JsonSerializer.Serialize(inputData);
    sw.WriteLine(jsonMessage);

    if (jsonMessage.Equals("close", StringComparison.OrdinalIgnoreCase))
    {
        keepStreaming = false;
        ns.Close();
        tcpClient.Close();
    }
    var JsonRespons = sr.ReadLine();
    Console.WriteLine(JsonRespons);
    string response = sr.ReadLine();
    Console.WriteLine("result: " + response);
}