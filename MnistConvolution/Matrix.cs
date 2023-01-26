namespace Convolutions.Matrix;

public static class Matrix
{
    private static float[] diagonal9x9 = null;
    public static float[] Diagonal9x9
    {
        get
        {
            if (diagonal9x9 == null)
            {
                diagonal9x9 = new float[] {
                    0, 0, 0, 0, 0, 0, 0, 0, .2f,
                    0, 0, 0, 0, 0, 0, 0, .2f, 0,
                    0, 0, 0, 0, 0, 0, .2f, 0, 0,
                    0, 0, 0, 0, 0, .2f, 0, 0, 0,
                    0, 0, 0, 0, .2f, 0, 0, 0, 0,
                    0, 0, 0, .2f, 0, 0, 0, 0, 0,
                    0, 0, .2f, 0, 0, 0, 0, 0, 0,
                    0, .2f, 0, 0, 0, 0, 0, 0, 0,
                    .2f, 0, 0, 0, 0, 0, 0, 0, 0,
                };
            }

            return diagonal9x9;
        }
    }


    public static float[] InvDiagonal9x9 { get; private set; } = new float[]{
        .2f, 0, 0, 0, 0, 0, 0, 0, 0,
        0, .2f, 0, 0, 0, 0, 0, 0, 0,
        0, 0, .2f, 0, 0, 0, 0, 0, 0,
        0, 0, 0, .2f, 0, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, 0, .2f, 0, 0, 0,
        0, 0, 0, 0, 0, 0, .2f, 0, 0,
        0, 0, 0, 0, 0, 0, 0, .2f, 0,
        0, 0, 0, 0, 0, 0, 0, 0, .2f,
    };

    public static float[] Vertical9x9 { get; private set; } = new float[]{
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
        0, 0, 0, 0, .2f, 0, 0, 0, 0,
    };

    public static float[] Horizontal9x9 { get; private set; } = new float[]{
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0,
    };

    public static float[] GenerateSquareMatrix(int m, float value, bool x, bool y, bool inv = false)
    {
        float[] returnMatrix = new float[m*m];

        for (int j = 0; j < m; j++)
        {
            for (int i = 0; i < m; i ++)
            {
                if (x && j == m/2)
                    returnMatrix[i + j * m] = value;
                else if (y && i == m/2)
                    returnMatrix[i + j * m] = value;
                else
                    returnMatrix[i + j * m] = 0;
            }
        }

        return returnMatrix;
    }

}