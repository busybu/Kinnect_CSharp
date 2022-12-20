using System;
using System.Linq;
[Serializable]
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

    public float[] OutputBefore(int before, params float[] inputs)
    {
        for (int i = 0; i < this.Layers.Length - before; i++)
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

    public float Score(DataSet ds)
    {
        float E = 0;
        foreach (var (X, Y) in ds)
        {
            var nx = N(X[i]); 
            for (int j = 0; j < X[0].Length; j++)
            {
                float value = nx[j] - Y[i][j];
                value = value * value;
                E += value;
            }
        }
        return E / (0.5f * ds.Length * ds.DataLength);
    }

    public void Fit(DataSet ds, int epochs = 100, float eta = 0.05f)
    {
        for (int i = 0; i < epochs; i++)
            this.epoch(ds, eta);
    }

    private void epoch(DataSet ds, float eta)
    {
        for (int l = 0; l < this.Layers.Length; l++)
        {
            for (int n = 0; n < this.Layers[l].Neurons.Length; n++)
            {
                var neuron = this.Layers[l].Neurons[n];

                var error = Score(ds);
                neuron.B += eta;
                var newError = Score(ds);
                if (newError > error)
                    neuron.B -= 2 * eta;
                error = newError;

                for (int w = 0; w < neuron.Ws.Length; w++)
                {
                    neuron.Ws[w] += eta;
                    newError = Score(ds);
                    if (newError > error)
                        neuron.Ws[w] -= 2 * eta;
                    error = newError;
                }
            }
        }
    }

    public float Fit()
    {
        return 1f;
    }
}