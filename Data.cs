using System;

public class Data
{
    public string Input1 { get; set; }
    public string Input2 { get; set; }
    public string Operation { get; set; }

    public Data(string? input1, string? input2, string? operation)
	{
        Input1 = input1;
        Input2 = input2;
        Operator = operation;
	}

    public override string ToString()
    {
        return "First number: " + Input1 + "Operator: " + Operator + "Second number: ";
    }
}
