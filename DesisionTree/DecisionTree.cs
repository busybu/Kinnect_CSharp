namespace DecisionTreeLib;

using System;
using System.Collections.Generic;
using Core;

public class DecisionTree
{
    public Node Root { get; private set; }
    private int dataLength { get; set; }
    public void Fit(int[,] x, int[] y, int minSample, int maxDepth)
    {
        this.Root = new Node();
        this.dataLength = x.GetLength(1);
        this.Root.Epoch(x, y, minSample, maxDepth);
    }
    public void Fit(List<int[]> x, int[] y, int minSample, int maxDepth)
    {
        this.Root = new Node();
        this.dataLength = x[0].Length;
        this.Root.Epoch(x, y, minSample, maxDepth);
    }
    public (int result, float probability) Choose(int[] data)
    {
        if (this.Root is null)
            throw new System.Exception("É necessario treinar antes de usar");

        if (this.dataLength != data.Length)
            throw new System.Exception("É necessario ter o mesmo número de dados usados para treinar");
        
        float result = Check(this.Root);
        
        if (result > 0.49)
            return (1, result);
        return (0, 1 - Check(this.Root));


        float Check(Node node)
        {
            if (node.Decision(data[node.ColumnIndex]))
            {
                if (node.Right is not null)
                    return Check(node.Right);
                return node.Probability;
            }
            else
            {
                if (node.Left is not null)
                    return Check(node.Left);
                return node.Probability;
            }
        }
    }
}