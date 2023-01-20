using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

public static class Binarization
{
    public static Bitmap ApplyBin(Bitmap bmp, Bitmap bg, float hparam = 0.05f)
    {
        var data_bmp = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        var data_bg = bg.LockBits(
            new Rectangle(0, 0, bg.Width, bg.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
        
        int histogramLen = (int)(1 / hparam) + 1;
        int[] hist = new int[histogramLen];
        int len = data_bg.Height * data_bg.Width;
        float medD = 0f;
        float sqSum = 0f;

        unsafe
        {
            byte* pDataBmp = (byte*)data_bmp.Scan0.ToPointer();
            byte* pDataBg = (byte*)data_bg.Scan0.ToPointer();

            for (int j = 0; j < data_bg.Height; j++)
            {
                byte* lBmp = pDataBmp + j * data_bmp.Stride;
                byte* lBkg = pDataBg + j * data_bg.Stride;

                for (int i = 0; i < data_bg.Width; i++, lBmp += 3, lBkg += 3)
                {
                    byte db = (byte)(lBkg[0] - lBmp[0]);
                    byte dg = (byte)(lBkg[1] - lBmp[1]);
                    byte dr = (byte)(lBkg[2] - lBmp[2]);

                    float d = (dr * dr + db * db + dg * dg) / (3f * 255f * 255f);
                    medD += d;
                    sqSum += d * d;

                    hist[(int)(d / hparam)]++;

                }
            }
        }

        medD = medD / len;

        float threshold = otsu(hist, medD, sqSum, len, hparam);

        unsafe
        {
            byte* pDataBmp = (byte*)data_bmp.Scan0.ToPointer();
            byte* pDataBg = (byte*)data_bg.Scan0.ToPointer();

            Parallel.For(0, data_bg.Height, j =>
            {
                byte* lBmp = pDataBmp + j * data_bmp.Stride;
                byte* lBkg = pDataBg + j * data_bg.Stride;

                for (int i = 0; i < data_bg.Width; i++, lBmp += 3, lBkg += 3)
                {
                    byte db = (byte)(lBkg[0] - lBmp[0]);
                    byte dg = (byte)(lBkg[1] - lBmp[1]);
                    byte dr = (byte)(lBkg[2] - lBmp[2]);

                    float d = (dr * dr + db * db + dg * dg) / (3f * 255f * 255f);
                    
                    if (d > threshold)
                    {
                        lBmp[0] = 255;
                        lBmp[1] = 255;
                        lBmp[2] = 255;
                    }
                    else
                    {
                        lBmp[0] = 0;
                        lBmp[1] = 0;
                        lBmp[2] = 0;
                    }
                }
            });
        }

        bmp.UnlockBits(data_bmp);
        bg.UnlockBits(data_bg);

        return bmp;
    }

    private static float otsu(int[] histogram, float med, float sqSum, int len, float hparam)
    {
        int threshold = 0;

        float Ex0 = 0;
        float Ex1 = med;
        float Dx0 = 0;
        float Dx1 = sqSum;
        int N0 = 0;
        int N1 = len;

        float minstddev = float.PositiveInfinity;

        for (int i = 0; i < histogram.Length; i++)
        {
            float value = hparam * (2 * i + 1) / 2;
            float s = histogram[i] * value;

            if (N0 == 0 && histogram[i] == 0)
                continue;

            Ex0 = (Ex0 * N0 + s) / (N0 + histogram[i]);
            Ex1 = (Ex1 * N1 - s) / (N1 - histogram[i]);

            N0 += histogram[i];
            N1 -= histogram[i];

            Dx0 += value * value * histogram[i];
            Dx1 -= value * value * histogram[i];

            float stddev =
                Dx0 - N0 * Ex0 * Ex0 +
                Dx1 - N1 * Ex1 * Ex1;

            if (float.IsInfinity(stddev) ||
                float.IsNaN(stddev))
                continue;

            if (stddev < minstddev)
            {
                minstddev = stddev;
                threshold = i;
            }
        }
        float bestTreshold = hparam * (2 * threshold + 1) / 2;

        return bestTreshold;
    }
}
