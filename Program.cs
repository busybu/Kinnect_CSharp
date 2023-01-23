using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DecisionTreeLib;

List<float[]> data = open()
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


int[] y = open()
    .Select(s => 
        s.Split(',')
        .Select(s => int.Parse(s))
        .Last()
    ).ToArray();




DecisionTree dt = new DecisionTree();
dt.Fit(x, y, 5, 60);

int count = 0;
for (int i = 0; i < x.Count; i++)
{
    if (dt.Choose(x[i]).result == y[i])
        count++;
}
Console.WriteLine(count);
Console.WriteLine(count * 1f / x.Count);



IEnumerable<string> open()
{
    StreamReader reader = new StreamReader("diabetes.csv");

    while (!reader.EndOfStream)
        yield return reader.ReadLine();

    reader.Close();
}