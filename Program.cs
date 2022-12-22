using System;

NeuralNetwork net = NeuralNetwork.Load("Data/IA_Pronta_5.sla"); //NeuralNetwork.Load("Data/IA_Pronta_1.sla");

DataSet data = DataSet.Load("Data/train.csv", "label");

net.Fit(data, 1);
net.Store("Data/IA_Pronta.sla");
// Console.WriteLine(net.Score(data));

// int nicekkk = 0;
// foreach (var (x, y) in data)
// {
//     if(y[net.Choose(x).Item1] > 0.5f)
//         nicekkk++;
// }
// Console.WriteLine(nicekkk);