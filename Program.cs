using System;

NeuralNetwork net = new NeuralNetwork(3, 3, 4, 2);

float[][] X = {
    new float[] { 1f, 1f, 1f },
    new float[] { 1f, 0f, 1f },
    new float[] { 0f, 1f, 1f },
    new float[] { 0f, 0f, 1f },
    new float[] { 1f, 0f, 0f },
    new float[] { 0f, 1f, 0f },
    new float[] { 1f, 1f, 0f },
    new float[] { 0f, 0f, 0f }
};

float[][] Y = {
    new float[] { 0f, 1f },
    new float[] { 0f, 1f },
    new float[] { 0f, 1f },
    new float[] { 1f, 0f },
    new float[] { 1f, 0f },
    new float[] { 1f, 0f },
    new float[] { 1f, 0f },
    new float[] { 1f, 0f }
};

DataSet data = new DataSet(X, Y);

net.Fit(data);
net.Store("salva.sla");