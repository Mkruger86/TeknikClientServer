using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

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
        string message = sr.ReadLine();
        Console.WriteLine("Client sent: " + message);
        if (message.Equals("close", StringComparison.OrdinalIgnoreCase))
        {
            keepListening = false;
            socket.Close();
        }
        int result = 0;
        string[] parts = message.Split(' ');
        if (parts.Length == 3)
        {
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[2]);
            switch (parts[1])
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





