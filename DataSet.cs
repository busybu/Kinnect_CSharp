using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class DataSet : IEnumerable<(float[], float[])>
{
    public float[][] X { get; private set; }
    public float[][] Y { get; private set; }
    public int Length => end - start;
    public int DataLength => X[0].Length;
    private int start;
    private int end;

    private DataSet() { }
    public DataSet(float[][] X, float[][] Y)
    {
        this.X = X;
        this.Y = Y;
        this.start = 0;
        this.end = this.X.Length;
    }
    
    public static DataSet Load(string path, string label)
    {
        var ds = new DataSet();
        
        var data = Open(path);
        int labelIndex = data.First().Split(',')
            .Select((item, index) => (item, index))
            .First(i => i.item == label).index;
        
        ds.start = 0;
        ds.end = data.Count() - 1;
        
        ds.X = new float[ds.Length][];
        ds.Y = new float[ds.Length][];
        int index = 0;

        foreach (var item in data.Skip(1))
        {
            string[] line = item.Split(',');
            var x = new float[line.Length - 1];
            var y = new float[10];
            int flag = 0;

            for (int i = 0; i < line.Length; i++)
            {
                int num = int.Parse(line[i]);

                if (i == labelIndex)
                {
                    y[num] = 1;
                    flag = 1;
                }
                else
                    x[i - flag] = num / 255f;
            }

            ds.X[index] = x;
            ds.Y[index] = y;
            index++;
        }

        return ds;

        IEnumerable<string> Open(string file)
        {
            var stream = new StreamReader(file);

            while (!stream.EndOfStream)
                yield return stream.ReadLine();

            stream.Close();
        }
    }

    public (DataSet, DataSet) Split(float pct)
    {
        DataSet ds1 = new DataSet(this.X, this.Y);
        DataSet ds2 = new DataSet(this.X, this.Y);
        
        int div = (int)(pct * this.DataLength);
        ds1.end = div;
        ds2.start = div;

        return (ds1, ds2);
    }

    public DataSet RandSplit(float pct)
    {
        Random rand = new Random();
        DataSet dataset = new DataSet(X, Y);
        int div = (int)(pct * this.DataLength);

        var start = rand.Next(0, this.DataLength - div);
        var end = rand.Next(start + div, this.DataLength);

        dataset.start = start;
        dataset.end = end;

        return dataset;
    }
    
    public IEnumerator<(float[], float[])> GetEnumerator()
    {
        for (int i = start; i < end; i++)
            yield return (X[i], Y[i]);
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}