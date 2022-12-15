public class Layer
{
    public Layer(Neuron[] neurons)
        => this.Neurons = neurons;

    public Neuron[] Neurons { get; set; }
    public float[] Output(float[] inputs)
    {
        float[] output = new float[this.Neurons.Length];
        for (int i = 0; i < output.Length; i++)
            output[i] = this.Neurons[i].Output(inputs);

        return output;
    }
}