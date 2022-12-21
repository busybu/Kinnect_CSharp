using System;

[Serializable]
public class Layer
{
    public Layer(Neuron[] neurons)
    {
        this.Neurons = neurons;
        this.Lenght = neurons.Length;
    }

    public Layer(int neuronsSize, int inputSize)
    {
        this.Neurons = new Neuron[neuronsSize];
        this.Lenght = neuronsSize;

        for (int i = 0; i < neuronsSize; i++)
            this.Neurons[i] = new Neuron(inputSize);
    }

    public Neuron[] Neurons { get; private set; }
    public int Lenght { get; private set; }
    public float[] Output(float[] xs)
    {
        float[] ys = new float[this.Lenght];
        
        for (int i = 0; i < ys.Length; i++)
            ys[i] = this.Neurons[i].Output(xs);

        return ys;
    }
}