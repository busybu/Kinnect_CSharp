using System.IO;

public class DataSet
{
    public float[][] X { get; private set; }
    public float[][] Y { get; private set; }
    private int start { get; set; } = 0;
    private int end { get; set; }
    public void Load_CSV(string path)
    {
        using (StreamReader sr = new StreamReader($"{path}.csv"))
        {
            string[] line = sr.ReadLine().Split(',');
            int length = line.Length - 1;
            this.X = new float[length][];
            this.Y = new float[length][];

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine().Split(',');
                for (int i = 0; i < length; i++)
                {
                    // sla[i] = float.Parse(line[i]) / 255;
                    
                }
            }
        }

    }
}