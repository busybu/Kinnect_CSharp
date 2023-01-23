namespace DecisionTreeLib;

using Core;

public class DecisionTree
{
    public Node Root { get; private set; }
    
    public bool Fit(int[,] x, int[] y, int minSample, int maxDepth)
    {
        this.Root = new Node();
        this.Root.Epoch(x, y, minSample, maxDepth);

        return true;
    }
}