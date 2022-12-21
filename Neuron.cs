using System;

[Serializable]
public class Neuron
{
    public Neuron(int weightsSize)
    {
        this.B = gaussianDist();
        this.Ws = new float[weightsSize];
        for (int i = 0; i < weightsSize; i++)
            this.Ws[i] = gaussianDist();
    }
    
    public Neuron(float bias, float[] weights)
    {
        this.B = bias;
        this.Ws = weights;
    }

    public Neuron()
    {
        this.B = B;    
        this.Ws = Ws;
    }

    public float B { get; set; }
    public float[] Ws { get; private set; }
    public float Output(float[] xs)
    {
        float y = this.B;
        for (int i = 0; i < xs.Length; i++)
            y += xs[i] * this.Ws[i];
        
        return sigma(y);
    }

    private float gaussianDist()
    {
        float sum = 0;
        for (int i = 0; i < 5; i++)
            sum += 2 * Random.Shared.NextSingle() - 1;
        
        return sum / 5;
    }

    private float sigma(float x)
        => 1f / (1f + MathF.Exp(-x));
}