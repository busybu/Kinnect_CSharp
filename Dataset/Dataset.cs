using System.Collections.Generic;
using System.Linq;
using System.IO;

public static class DataSet
{
    public static (List<int[]> x, int[] y) ReadCSV(string path)
    {

        int[] y = open(path)
            .Select(s => 
                int.Parse(
                    s.Split(',')
                    .Last())
            ).ToArray();

        List<float[]> data = open(path)
            .Select(s => 
                s.Split(',')
                    .Select(s => float.Parse(s.Replace('.', ',')))
                    .Reverse()
                    .Skip(1)
                    .Reverse()
                    .ToArray()
        ).ToList();

        float[] max = new float[data[0].Length];
        float[] min = new float[data[0].Length];

        for (int i = 0; i < data[0].Length; i++)
        {
            max[i] = data.Select(a => a[0]).Max();
            min[i] = data.Select(a => a[0]).Min();
        }

        var x = data
        .Select(a => 
            a.Select((x, i) => 
                (int)(20 * (x - min[i]) / (max[i] - min[i])))
                .ToArray())
        .ToList();

        return (x, y);        
    }
    private static IEnumerable<string> open(string file)
    {
        var stream = new StreamReader(file);

        while (!stream.EndOfStream)
            yield return stream.ReadLine();

        stream.Close();
    }
}