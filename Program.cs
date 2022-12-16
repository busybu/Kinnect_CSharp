NeuralNetwork net = new NeuralNetwork(2, 4, 4, 2);


float[][] X = {
    new float[] { 0f, 0f },
    new float[] { 0f, 1f },
    new float[] { 1f, 0f },
    new float[] { 1f, 1f },
};

float[][] Y = {
    new float[] { 1f, 0f },
    new float[] { 1f, 0f },
    new float[] { 1f, 0f },
    new float[] { 0f, 1f },
};

System.Console.WriteLine(net.Score(X, Y));