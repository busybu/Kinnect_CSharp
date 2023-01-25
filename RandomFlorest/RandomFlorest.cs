namespace RandomFlorestLib;

using System.IO;
using System.Linq;
using System.Text;
using DecisionTreeLib;

public class RandomFlorest
{
    public DecisionTree[] DecisionTrees { get; private set; }
    public void Fit()
    {
        DecisionTree[] dtArr = new DecisionTree[10];
        for (int i = 0; i < 10; i++)
        {
            DecisionTree dt = new DecisionTree();
            var data = DataSet.ReadCSV($"Data/number{i}.csv");
            
            dt.Fit(data.x, data.y, 5, 60);

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

    public void Save(string path)
    {
        string txt = "";
        using (FileStream fs = File.Create(path))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(txt);

            fs.Write(info, 0, info.Length);
        }
    }
}