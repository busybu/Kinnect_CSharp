using System;
using System.Linq;

DataSet data = DataSet.Load("Data/train.csv", "label");

// for (int i = 0; i <= 16; i++)
// {
//     NeuralNetwork net = NeuralNetwork.Load($"Data/IA_Pronta_{i}.sla"); //NeuralNetwork.Load("Data/IA_Pronta_1.sla");
    // net.Fit(data, 1);
    // net.Store($"Data/IA_Pronta_{7 + i}.sla");
//     Console.WriteLine($"IA_Pronta_{i}: {net.Score(data)}");
// }


NeuralNetwork net = NeuralNetwork.Load("Data/IA_Pronta_16.sla");
int count = 0;
foreach (var (x, y) in data)
{
    var output = net.Output(x);
    var result = net.Choose(x);
    // Console.Write(y.Zip(output).Aggregate("", (s, z) => s + z.ToString() + ", "));
    if (result.Item1 == y.First(i => i == 1))
        count++;
}
Console.WriteLine(1f * count / data.Length);