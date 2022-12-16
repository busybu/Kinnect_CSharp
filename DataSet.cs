using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

public class DataSet
{
    public float[][] X { get; private set; }
    public float[][] Y { get; private set; }
    private int start { get; set; } = 0;
    private int end { get; set; }
    public void Load_CSV(string path)
    {
        var data = Open(path).Skip(1);
        this.X = new float[data.Count()][];
        this.Y = new float[data.Count()][];
        int index = 0;

        fo      reach (var item in data)
        {
            string[] line = item.Split(',');
            var x = new float[line.Length - 1];
            var y = new float[10];

            for (int i = 0; i < line.Length; i++)
            {
                int num = int.Parse(line[i]);

                if (i == 0)
                {
                    for (int j = 0; j < y.Length; j++)
                    {
                        if (j == num)
                            y[j] = 1;
                        else
                            y[j] = 0;
                    }
                }
                else
                    x[i - 1] = num;
            }

            this.X[index] = x;
            this.Y[index] = y;
            index++;
            System.Console.WriteLine(X);
        }
        // using (StreamReader sr = new StreamReader(path))
        // {
        //     string[] line = sr.ReadLine().Split(',');
        //     int length = line.Length - 1;
        //     this.X = new float[length];
        //     this.Y = new float[length];
        //     int indice = line.Select((item, index) => (item, index)).First(i => i.item == label).index;
        //     System.Console.WriteLine(indice);
        //     while (!sr.EndOfStream)
        //     {
        //         line = sr.ReadLine().Split(',');
        //         for (int i = 0; i < length; i++)
        //         {
        //             if(i == indice) 
        //                 this.Y = i;
        //         }
        //         break;
        //     }
        // }
    }
    private IEnumerable<string> Open(string file)
    {
        var stream = new StreamReader(file);

        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();
            yield return line;
        }

        stream.Close();
    }
}
