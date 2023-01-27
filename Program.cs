using RandomFlorestLib;
using DecisionTreeLib;
using System;

// for (int i = 0; i < 10; i++)
// {
//     var data = DataSet.ReadCSV($"Data/number{i}.csv");
//     DecisionTree dt = new DecisionTree();
    // dt.Fit(data.x, data.y, 10, 30);
//     dt.Save($"Trees/Tree{i}.cs", i);
// }





var data = DataSet.ReadCSV($"Data/a.csv");
var x = data.x;
var y = data.y;
// RandomFlorest rfr = new RandomFlorest();
// rfr.Fit();
// rfr.Store("Data/Finalizado.sla");
RandomFlorest rfr = RandomFlorest.Load("Data/Finalizado.sla");
var test = DataSet.ReadCSV($"Data/b.csv");
x = test.x;
y = test.y;

int count = 0;
for (int i = 0; i < x.Count; i++)
{
    if (rfr.Choose(x[i]) == y[i])
        count++;
}

Console.WriteLine(count * 1f / x.Count);