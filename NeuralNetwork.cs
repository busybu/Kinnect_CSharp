using System.Linq;

public class NeuralNetwork
{
    public NeuralNetwork(Layer[] layer) =>
        this.Layers = layer;
    
    public NeuralNetwork(params int[] neuronSize)
    {
        this.Layers = new Layer[neuronSize.Length - 1];

        for (int i = 0; i < neuronSize.Length - 1; i++)
            this.Layers[i] = new Layer(neuronSize[i + 1], neuronSize[i]);
    }

    public Layer[] Layers { get; private set; }

    public float[] N(params float[] inputs)
    {
        for (int i = 0; i < this.Layers.Length; i++)
            inputs = this.Layers[i].Output(inputs);

        return inputs;
    }
    public (int, float) Choose(params float[] inputs)
    {
        for (int i = 0; i < this.Layers.Length; i++)
            inputs = this.Layers[i].Output(inputs);

        return inputs
            .Select((input, index) => (index, input))
            .MaxBy(i => i.input);
    }

    public float Score(float[][] X, float[][] Y)
    {
        float E = 0;
        for (int i = 0; i < X.Length; i++)
        {
            var nx = N(X[i]); 
            for (int j = 0; j < X[0].Length; j++)
            {
                float value = nx[j] - Y[i][j];
                value = value * value;
                E += value;
            }
        }
        return E / (0.5f * X.Length * X[0].Length);
    }

    public float Fit()
    {
        return 1f;
    }
}