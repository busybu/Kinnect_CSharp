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

    public float[] Output(params float[] inputs)
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

    public float Score(float[][] X, float[][] Y)
    {
        float E = 0;
        for (int i = 0; i < X.Length; i++)
        {
            var outputX = this.Output(X[i]);
            for (int j = 0; j < outputX.Length; j++)
            {
                float value = outputX[j] - Y[i][j];
                value = value * value;
                E += value;
            }
        }
        return E / (0.5f * X.Length * X[0].Length);
    }

    public void Fit(float[][] X, float[][] Y, int epochs = 100, float eta = 0.05f)
    {
        for (int i = 0; i < epochs; i++)
            this.epoch(X, Y, eta);
    }

    private void epoch(float[][] X, float[][] Y, float eta)
    {
        for (int l = 0; l < this.Layers.Length; l++)
        {
            for (int n = 0; n < this.Layers[l].Neurons.Length; n++)
            {
                var neuron = this.Layers[l].Neurons[n];

                var error = Score(X, Y);
                neuron.B += eta;
                var newError = Score(X, Y);
                if (newError > error)
                    neuron.B -= 2 * eta;
                error = newError;

                for (int w = 0; w < neuron.Ws.Length; w++)
                {
                    neuron.Ws[w] += eta;
                    newError = Score(X, Y);
                    if (newError > error)
                        neuron.Ws[w] -= 2 * eta;
                    error = newError;
                }
            }
        }
    }

    private void f_epoch(float[][] X, float[][] Y, float eta = 0.01f)
    {
        Layer lastLayer = this.Layers.Last();
        float[] delta = new float[lastLayer.Neurons.Length];
        for (int i = 0; i < lastLayer.Neurons.Length; i++)
        {
            for (int j = 0; j < X.Length; j++)
            {
                float[] Z = this.Output(X[j]);
                delta[i] += (Z[i] - Y[j][i]) * Z[i] * (1 - Z[i]);
            }

            Neuron neuron = lastLayer.Neurons[i];
            neuron.B -= eta * delta[i];
        }

        int wcount = lastLayer.Neurons.First().Ws.Length;
        for (int i = 0; i < lastLayer.Neurons.Length; i++)
        {
            Neuron neuron = lastLayer.Neurons[i];
            for (int w = 0; w < wcount; w++)
            {
                float wderiv = 0;
                for (int x = 0; x < X.Length; x++)
                {
                    float[] Z = this.OutputBefore(1, X[x]);
                    wderiv += Z[w] * delta[i];
                }
                neuron.Ws[w] -= eta * wderiv;
            }
        }
    }
}