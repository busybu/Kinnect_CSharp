using System;

NeuralNetwork net = new NeuralNetwork(784, 100, 40, 10);

DataSet data = DataSet.Load("Data/train.csv", "label");

net.Fit(data, 1);
net.Store("Data/IA_Pronta.sla");