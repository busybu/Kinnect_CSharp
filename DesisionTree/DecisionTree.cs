namespace DecisionTreeLib;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

    public float Choose(int[] data)
    {
        if (this.Root is null)
            throw new System.Exception("É necessario treinar antes de usar");

        if (this.dataLength != data.Length)
            throw new System.Exception("É necessario ter o mesmo número de dados usados para treinar");
        
        float result = Check(this.Root);
        
        return result;


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

    public void Save(string path)
    {
        string txt = "public float Choose(int[] data)\n{\n";
        txt += Append(this.Root, 1, "if");
        Console.WriteLine(txt.Length);

        if (File.Exists(path))
            File.Delete(path);

        using (FileStream fs = File.Create(path))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(txt + "\n}");

            fs.Write(info, 0, info.Length);
        }

        string Append(Node node, int tab, string type)
        {
            string tablacao = String.Concat(Enumerable.Repeat("\t", tab)),
                   comparation = type == "if" ? $"if (data[{node.ColumnIndex}] {ComparisonString(node.Comparison)} {node.Target})" : "else",
                   code = tablacao + comparation + $"\n{tablacao}" + "{\n";
            
            if (node.Right is not null)
                code += Append(node.Right, tab + 1, "if");
                
            if (node.Left is not null)
                code += Append(node.Left, tab + 1, "else");

            return code += tablacao + $"\treturn {node.Probability};\n".Replace(',', '.') + tablacao + "}\n";
        }

        string ComparisonString(ComparisonSigns comparison)
        {
            switch (comparison)
            {
                case ComparisonSigns.Equal:
                    return "=";

                case ComparisonSigns.Bigger:
                    return ">";

                case ComparisonSigns.BiggerEqual:
                    return ">=";

                case ComparisonSigns.Less:
                    return "<";

                case ComparisonSigns.LessEqual:
                    return "<=";

                default:
                    return "!=";
            }
        }
    }
}