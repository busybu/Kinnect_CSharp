using System;
using System.IO;
using System.Linq;


NeuralNetwork net = new NeuralNetwork(785, 800, 800, 10);

using (StreamReader sr = new StreamReader("train.csv"))
{
    string[] line = sr.ReadLine().Split(',');

    while (!sr.EndOfStream)
    {
        line = sr.ReadLine().Split(',');
        float[] sla = new float[line.Length];
        for (int i = 0; i < sla.Length; i++)
            sla[i] = float.Parse(line[i]) / 255;
        
        Console.WriteLine(net.Output(sla));
    }
}


