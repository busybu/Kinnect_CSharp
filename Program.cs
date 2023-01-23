using DecisionTreeLib;

int[,] x = {
    {0,0},
    {0,0},
    {0,2},
    {1,0},
    {1,2},
    {2,1}
};

int[] y = {
    0,
    1,
    1,
    1,
    1,
    0
};


DecisionTree dt = new DecisionTree();
dt.Fit(x, y, 2, 6);