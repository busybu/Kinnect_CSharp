NeuralNetwork net = new NeuralNetwork(3, 4, 2);

DataSet data = new DataSet();


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
    new float[] { 0f, 0f },
    new float[] { 0f, 0f },
    new float[] { 0f, 0f },
    new float[] { 0f, 0f },
    new float[] { 0f, 0f }
};

// System.Console.WriteLine(net.Score(X, Y));

SerialTest st = new SerialTest();
st.SerializeNow();

