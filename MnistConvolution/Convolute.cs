namespace Convolutions.Core;

using Matrix;

public static class Convolutions
{
    public static byte[] Convolute (byte[] img, float[] kernel, int Width, int Height)
    {
        var N = (int)Math.Sqrt(kernel.Length);
        var wid = Width;
        var hei = Height;
        var _img = img;

        var ladin = (N - 1);
        int index = 0;

        byte[] result = new byte[(wid - ladin) * (hei - ladin)];

        for (int j = N / 2; j < hei - N / 2; j++)
        {
            for (int i = N / 2; i < wid - N / 2; i++)
            {
                float sum = 0;

                for (int k = 0; k < N; k++)
                {
                    for (int l = 0; l < N; l++)
                    {
                        sum += _img[i + k - (N / 2) + (j + l - (N / 2)) * wid] *
                        kernel[k + l * N];
                    }
                }
                if (sum > 255)
                    sum = 255;
                else if (sum < 0)
                    sum = 0f;

                result[index] = (byte)sum;
                index++;
            }
        }

        return result;
    }

    public static byte[] Sobel(byte[] img, int width, int height)
    {
        byte[] result = new byte[img.Length];
        byte[] realResult = new byte[img.Length];


        float soma;
        float total;

        for (int j = 1; j < height - 1; j++)
        {
            for (int i = 1; i < width - 1; i++)
            {
                soma = img[(i + (j * width)) - 1] + img[(i + (j * width))];
                total = soma;
                soma = img[(i + (j * width))] + img[(i + (j * width)) + 1];
                total -= soma;
                result[i + (j * width)] = (byte)total;
            }
        }

        soma = img[0] + img[1] + img[2];
        for (int i = 2; i < width - 1; i++)
        {
            for (int j = 1; j < height - 1; j++)
            {
                soma -= result[(i + (j * width)) - 2];
                soma += result[(i + (j * width)) + 1];

                realResult[i + (j * width)] = (byte)soma;
            }
        }
        return realResult;
    }

    public static byte[] Convolute4 (byte[] img, int width, int height)
    {
        byte[] arr = new byte[100];
        Mnist item = new Mnist();
        item.img = img;

        int index = 0;
        
        item.imgR = Convolute(item.img, Matrix.Horizontal9x9, width, height);
        item.Reduce();
        for (int i = 0; i < item.imgReduced.Length; i++, index++)
            arr[index] = item.imgReduced[i];
    
        item.imgR = Convolute(item.img, Matrix.Vertical9x9, width, height);
        item.Reduce();
        for (int i = 0; i < item.imgReduced.Length; i++, index++)
            arr[index] = item.imgReduced[i];

        item.imgR = Convolute(item.img, Matrix.Diagonal9x9, width, height);
        item.Reduce();
        for (int i = 0; i < item.imgReduced.Length; i++, index++)
            arr[index] = item.imgReduced[i];

        item.imgR = Convolute(item.img, Matrix.InvDiagonal9x9, width, height);
        item.Reduce();
        for (int i = 0; i < item.imgReduced.Length; i++, index++)
            arr[index] = item.imgReduced[i];

        return arr;
    }

}
