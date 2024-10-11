using System;

namespace EchoServer
{
    public class InputData
    {
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Operation { get; set; }

        public InputData()
        {
        }
        public InputData(string? input1, string? input2, string? operation)
        {
            Input1 = input1;
            Input2 = input2;
            Operation = operation;
        }

        public override string ToString()
        {
            return "First number: " + Input1 + " Operator: " + Operation + " Second number: " + Input2 + "\n\n";
        }
    }
}
