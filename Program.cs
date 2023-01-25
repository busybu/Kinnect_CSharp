using RandomFlorestLib;
using DecisionTreeLib;
using System;

for (int i = 0; i < 10; i++)
{
    var data = DataSet.ReadCSV($"Data/number{i}.csv");
    // Naosei ns = new Naosei();
    // ns.Choose(data.x[0]);
    DecisionTree dt = new DecisionTree();
    dt.Fit(data.x, data.y, 5, 60);
    dt.Save($"Test/test{i}.cs");
    // break;
}




// RandomFlorest rfr = new RandomFlorest();
// rfr.Fit();

// int count = 0;
// for (int i = 0; i < x.Count; i++)
// {
//     if (rfr.Choose(x[i]) == y[i])
//         count++;
// }

// Console.WriteLine(count * 1f / x.Count);