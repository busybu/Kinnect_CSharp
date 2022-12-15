using System;
public class Neuron
{
    public Neuron(float b, float[] weights)
    {
        this.B = b;
        this.weights = weights;
    }

    private float B { get; set; }
    private float[] weights { get; set; }
    public float Output(float[] inputs)
    {
        float output = this.B;
        for (int i = 0; i < inputs.Length; i++)
            output += inputs[i] * this.weights[i];
        
        return 1 / (1 + MathF.Pow(MathF.E, -output));
    }
}