namespace RandomFlorestLib;

using System.Runtime.Serialization.Formatters.Binary;
using DecisionTreeLib;
using System.Text;
using System.Linq;
using System.IO;

[System.Serializable]
public class RandomFlorest
{
    public DecisionTree[] DecisionTrees { get; private set; }
    public void Fit()
    {
        DecisionTree[] dtArr = new DecisionTree[10];
        for (int i = 0; i < 10; i++)
        {
            DecisionTree dt = new DecisionTree();
            var data = DataSet.ReadCSV($"IA/Data/number{i}.csv");
            
            dt.Fit(data.x, data.y, 8, 40);

            dtArr[i] = dt;
        }

        this.DecisionTrees = dtArr;
    }

    public int Choose(int[] data)
    {
        (int value, float probability)[] results = new (int result, float probability)[this.DecisionTrees.Length];

        for (int i = 0; i < this.DecisionTrees.Length; i++)
            results[i] = (i, this.DecisionTrees[i].Choose(data));

        return results.MaxBy(r => r.probability).value;
    }
    public int Choose(byte[] data)
    {
        byte max = data.Max(),
             min = data.Min();

        var x = data
            .Select(a => (int)(20 * (a - min) / (max - min)))
            .ToArray();
        
        return this.Choose(x);
    }

    public void Save(string path)
    {
        string txt = "";
        using (FileStream fs = File.Create(path))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(txt);

            fs.Write(info, 0, info.Length);
        }
    }

    public void Store(string path) 
    {
        FileStream file = File.Create(path); 
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, this);
        file.Close();
    }

    public static RandomFlorest Load(string path)
    {
        FileStream file = File.Open(path, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        RandomFlorest rfr = (RandomFlorest)formatter.Deserialize(file);
        file.Close();

        return rfr;
    }
}