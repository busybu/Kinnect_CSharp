namespace DecisionTreeLib.Core;

using System;
using System.Collections.Generic;
using System.Linq;

public class Node
{
    public Node Left { get; private set; }
    public Node Right { get; private set; }
    public ComparisonSigns Comparison { get; private set; } = ComparisonSigns.Bigger;
    public int Target { get; private set; }
    public int ColumnIndex { get; private set; }
    
    public void Epoch(int[,] x, int[] y, int minSample, int maxDepth)
    {
        if (maxDepth == 0 || x.GetLength(0) < minSample)
            return;

        float[] I = this.EntropiaDeInformacao(x, y),
                C = this.EntropiaDeConteudo(x, y),
                G = new float[I.Length];
        
        for (int i = 0; i < G.Length; i++)
            G[i] = I[i] / C[i];

        int colIndex = G
            .Select((num, index) => (num, index))
            .MaxBy(e => e.num)
            .index;
        
        this.ColumnIndex = colIndex;

        this.Target = this.SelectTarget(x.GetLength(0), GetColumn(x, colIndex));

        List<int[]> leftX = new List<int[]>(),
                    rightX = new List<int[]>();
        
        List<int> leftY = new List<int>(),
                  rightY = new List<int>();
        
        for (int i = 0; i < x.GetLength(0); i++)
        {
            if (this.Decision(x[i, colIndex]))
            {
                rightX.Add(GetRow(x, i));
                rightY.Add(y[i]);
            }
            
            else
            {
                leftX.Add(GetRow(x, i));
                leftY.Add(y[i]);
            }
        }

        this.Left = new Node();
        this.Right = new Node();
        
        this.Left.Epoch(convertToMatrix(leftX), leftY.ToArray(), minSample, maxDepth - 1);
        this.Right.Epoch(convertToMatrix(rightX), rightY.ToArray(), minSample, maxDepth - 1);
    }

    private int[,] convertToMatrix(List<int[]> list)
    {
        int[,] result = new int[list.Count, list[0].Length-1];
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[0].Length-1; j++)
                result[i,j] = list[i][j];
        }
        return result;
    }

    public int SelectTarget(int n, int[] col)
    {
        int index = Random.Shared.Next(n/4, n - n/4);
        return col[index];
    }

    public float[] EntropiaDeInformacao(int[,] x, int[] y)
    {
        float n = y.Count(),
              trueValues = y.Count(i => i == 1) / n,
              falseValues = y.Count(i => i == 0) / n,
              E0 =  -(trueValues * MathF.Log2(trueValues)) +
                    -(falseValues * MathF.Log2(falseValues));
        
        float[] result = new float[x.GetLength(1)];
        
        for (int j = 0; j < x.GetLength(1); j++)
        {
            int[] col = GetColumn(x, j);
            var attrs = col.Distinct().ToArray();
            float ECol = 0;

            for (int index = 0; index < attrs.Length; index++)
            {
                var attr = attrs[index];

                float attrCount = 0,
                      trueCount = 0,
                      falseCount = 0;
                    
                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i] == attr)
                    {
                        attrCount++;
                        if (y[i] == 1)
                            trueCount++;
                        else
                            falseCount++;
                    }
                }

                if (trueCount == 0 || falseCount == 0 ||
                    trueCount == attrCount || falseCount == attrCount)
                    continue;
                
                float total = (attrCount / n),
                      trueTotal = -((trueCount / attrCount) * MathF.Log2(trueCount / attrCount)),
                      falseTotal = -((falseCount / attrCount) * MathF.Log2(falseCount / attrCount)),
                      temp = total * (trueTotal + falseTotal);
                
                ECol += temp;
            }

            result[j] = E0 - ECol;
        }

        return result;
    }


    public float[] EntropiaDeConteudo(int[,] x, int[] y)
    {
        float n = y.Count();
        
        float[] result = new float[x.GetLength(1)];

        for (int j = 0; j < x.GetLength(1); j++)
        {
            int[] col = this.GetColumn(x, j);
            var attrs = col.Distinct().ToArray();
            float ECol = 0;

            for (int index = 0; index < attrs.Length; index++)
            {
                var attr = attrs[index];

                float attrCount = 0;
                    
                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i] == attr)
                        attrCount++;
                }
                float total = attrCount / n,
                      temp = -total * MathF.Log2(total);

                ECol += temp;
            }

            result[j] = ECol;
        }
        return result;
    }

    public bool Decision(int value)
    {
        switch (this.Comparison)
        {
            case ComparisonSigns.Equal:
                return value == this.Target;

            case ComparisonSigns.Bigger:
                return value > this.Target;

            case ComparisonSigns.BiggerEqual:
                return value >= this.Target;

            case ComparisonSigns.Less:
                return value < this.Target;

            case ComparisonSigns.LessEqual:
                return value <= this.Target;

            default:
                return value != this.Target;
        }
    }

    private int[] GetColumn(int[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    private int[] GetRow(int[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}