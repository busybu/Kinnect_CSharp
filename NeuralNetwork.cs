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

    public (int, float) Output(params float[] inputs)
    {
        for (int i = 0; i < this.Layers.Length; i++)
            inputs = this.Layers[i].Output(inputs);

        return inputs
            .Select((input, index) => (index, input))
            .MaxBy(i => i.input);
    }
}