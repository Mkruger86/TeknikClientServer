using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using EchoServer;
using System.Threading.Tasks;
using System.Globalization;

Console.WriteLine("EchoServerTest started. Press enter to begin");
Console.ReadLine();

TcpListener listener = new TcpListener(IPAddress.Any, 4000);
listener.Start();
Console.WriteLine("Waiting for connection (start EchoClientTest to proceed)");

while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    Task.Run(() => HandleClient(socket));
    Console.WriteLine("Connection established");
}
void HandleClient(TcpClient socket)
{
    NetworkStream ns = socket.GetStream();
    StreamReader sr = new StreamReader(ns);
    StreamWriter sw = new StreamWriter(ns);
    bool keepListening = true;
    while (keepListening)
    {
        var message = sr.ReadLine();
        var newmessage = JsonSerializer.Deserialize<InputData>(message);
        Console.WriteLine("Client sent: " + newmessage);
        if (newmessage.Operation.Equals("close", StringComparison.OrdinalIgnoreCase))
        {
            keepListening = false;
            socket.Close();
        }
        int result = 0;
        int a = int.Parse(newmessage.Input1);
        int b = int.Parse(newmessage.Input2);
        if (newmessage.Operation != null)
        {
            switch (newmessage.Operation)
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    result = a / b;
                    break;
                case "random":
                    result = new Random().Next(a, b);
                    break;
                default:
                    break;
            }
            string equationResult = result.ToString();
            sw.WriteLine(equationResult);
            sw.WriteLine(message);
            sw.Flush();
        }
        else
        {
            sw.WriteLine("Invalid equation");
            sw.Flush();
        }
    }
    socket.Close();
}
