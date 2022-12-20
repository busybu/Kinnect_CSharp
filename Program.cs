using System;

NeuralNetwork net = new NeuralNetwork(784, 100, 40, 10);

DataSet data = DataSet.Load("train.csv", "label");

net.Fit(data);
net.Store("salva.sla");