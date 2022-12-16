using System;
using System.IO;
using System.Linq;


NeuralNetwork net = new NeuralNetwork(785, 800, 800, 10);

DataSet data = new DataSet();

data.Load_CSV("train.csv");