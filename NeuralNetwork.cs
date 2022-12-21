using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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

    public float[] Output(params float[] xs)
    {
        for (int i = 0; i < this.Layers.Length; i++)
            xs = this.Layers[i].Output(xs);

        return xs;
    }

    public float[] PreviusOutput(int previus, params float[] xs)
    {
        for (int i = 0; i < this.Layers.Length - previus; i++)
            xs = this.Layers[i].Output(xs);

        return xs;
    }

    public (int, float) Choose(params float[] xs)
    {
        for (int i = 0; i < this.Layers.Length; i++)
            xs = this.Layers[i].Output(xs);

        return xs
            .Select((input, index) => (index, input))
            .MaxBy(i => i.input);
    }

    public float Score(DataSet ds)
    {
        float E = 0;
        foreach (var (X, Y) in ds)
        {
            var Z = this.Output(X);
            for (int i = 0; i < Z.Length; i++)
            {
                float value = Z[i] - Y[i];
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

                var error = Score(ds.RandSplit(0.001f));
                neuron.B += eta;
                var newError = Score(ds.RandSplit(0.001f));

                if (newError > error)
                    neuron.B -= 2 * eta;
                error = newError;

                for (int w = 0; w < neuron.Ws.Length; w++)
                {
                    neuron.Ws[w] += eta;
                    newError = Score(ds.RandSplit(0.001f));

                    if (newError > error)
                        neuron.Ws[w] -= 2 * eta;

                    error = newError;
                }
            }
        }
    }

    private void f_epoch(DataSet ds, float eta = 0.01f)
    {
        Layer lastLayer = this.Layers.Last();
        float[] delta = new float[lastLayer.Neurons.Length];
        for (int i = 0; i < lastLayer.Neurons.Length; i++)
        {
            for (int j = 0; j < ds.Length; j++)
            {
                float[] Z = this.Output(ds.X[j]);
                delta[i] += (Z[i] - ds.Y[j][i]) * Z[i] * (1 - Z[i]);
            }

            Neuron neuron = lastLayer.Neurons[i];
            neuron.B -= eta * delta[i];
        }

        int wcount = lastLayer.Neurons[0].Ws.Length;
        for (int i = 0; i < lastLayer.Neurons.Length; i++)
        {
            Neuron neuron = lastLayer.Neurons[i];
            for (int w = 0; w < wcount; w++)
            {
                float wderiv = 0;
                for (int x = 0; x < ds.Length; x++)
                {
                    float[] Z = this.PreviusOutput(1, ds.X[x]);
                    wderiv += Z[w] * delta[i];
                }
                neuron.Ws[w] -= eta * wderiv;
            }
        }
    }

    public void Store(string path) 
    {
        FileStream file = File.Create(path); 
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, this);
        file.Close();
    }

    public static NeuralNetwork Load(string path)
    {
        NeuralNetwork net = null;

        FileStream file = File.Open(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        net = (NeuralNetwork)formatter.Deserialize(file);
        file.Close();

        return net;
    }
}