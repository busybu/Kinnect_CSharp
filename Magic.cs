using System.Drawing.Imaging;

public static class Abra
{
    public static int[] Kadabra(Bitmap bmp, Bitmap bg, float bgParam, float treshold, int radius)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        var bgData = bg.LockBits(
            new Rectangle(0, 0, bg.Width, bg.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        float param = getParam(data);
        normalize(data, param, bgParam);
        blur(data, radius);
        var hist = applyBin2(data, bgData);

        bmp.UnlockBits(data);
        bg.UnlockBits(bgData);

        return hist;
    }

    private static void normalize(BitmapData data, float param, float bgParam)
    {
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            float cal = bgParam / param;
            Parallel.For(0, data.Height, j =>
            {
                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width; i++, line += 3)
                {
                    line[0] = line[0] * cal > 255 ? (byte)255 : (byte)(line[0] * cal);
                    line[1] = line[1] * cal > 255 ? (byte)255 : (byte)(line[1] * cal);
                    line[2] = line[2] * cal > 255 ? (byte)255 : (byte)(line[2] * cal);
                }
            });
        }
    }

    private static float getParam(BitmapData data)
    {
        long R = 0;
        long G = 0;
        long B = 0;
        long C = 0;

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            Parallel.For(0, data.Height, j =>
            {
                long _R = 0;
                long _G = 0;
                long _B = 0;
                long _C = 0;

                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width / 10; i++, line += 3)
                {
                    byte b = line[0];
                    byte g = line[1];
                    byte r = line[2];

                    _R += r;
                    _G += g;
                    _B += b;
                    _C++;
                }

                R += _R;
                G += _G;
                B += _B;
                C += _C;
            });
        }

        return (R + G + B) / (float)C;
    }

    private static Bitmap applyBin(BitmapData dataBmp, BitmapData dataBg, float treshold = .001f)
    {

        var returnBmp = new Bitmap(dataBmp.Width, dataBmp.Height);
        var dataRt = returnBmp.LockBits(
            new Rectangle(0, 0, returnBmp.Width, returnBmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* pDataBmp = (byte*)dataBmp.Scan0.ToPointer();
            byte* pDataBg = (byte*)dataBg.Scan0.ToPointer();
            byte* pDataReturn = (byte*)dataRt.Scan0.ToPointer();

            for (int j = 0; j < dataBg.Height; j++)
            {
                byte* lBmp = pDataBmp + j * dataBmp.Stride;
                byte* lBkg = pDataBg + j * dataBg.Stride;
                byte* lRet = pDataReturn + j * dataRt.Stride;

                for (int i = 0; i < dataBg.Width; i++, lBmp += 3, lBkg += 3, lRet += 3)
                {
                    int db = lBkg[0] - lBmp[0];
                    int dg = lBkg[1] - lBmp[1];
                    int dr = lBkg[2] - lBmp[2];

                    float diff = (dr * dr + db * db + dg * dg) / (3f * 255 * 255);

                    if (diff > treshold)
                    {
                        lRet[0] = 0;
                        lRet[1] = 0;
                        lRet[2] = 0;
                    }
                    else
                    {
                        lRet[0] = 255;
                        lRet[1] = 255;
                        lRet[2] = 255;
                    }
                }
            }
        }

        returnBmp.UnlockBits(dataRt);

        return returnBmp;
    }

    private static long[] integralImage = null;

    private static void blur(BitmapData data, int radius = 5)
    {
        if (integralImage == null)
            setBuffer(data);

        int N = radius * 2 + 1;
        int A = N * N;

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            Parallel.For(0, data.Height, j =>
            {
                byte* l = p + j * data.Stride;
                for (int i = 0; i < data.Width; i++, l += 3)
                {
                    byte r = (byte)((30 * l[2] + 59 * l[1] + 11 * l[0]) / 100);
                    l[0] = r;
                    l[1] = r;
                    l[2] = r;
                }
            });

            int width = data.Width;

            integralImage[0] = p[0];

            for (int i = 1; i < width; i++)
            {
                byte* l = p + 3 * i;
                integralImage[i] = l[0] + integralImage[i - 1];
            }

            for (int j = 1; j < data.Height; j++)
            {
                byte* l = p + j * data.Stride;
                integralImage[j * width] = l[0] + integralImage[(j - 1) * width];
            }

            for (int j = 1; j < data.Height; j++)
            {
                byte* l = p + j * data.Stride;
                for (int i = 1; i < width; i++, l += 3)
                {
                    integralImage[i + j * width] = l[0]
                        + integralImage[i + (j - 1) * width]
                        + integralImage[i - 1 + j * width]
                        - integralImage[i - 1 + (j - 1) * width];
                }
            }

            Parallel.For(radius + 1, data.Height - radius, j =>
            {
                byte* l = p + j * data.Stride + 3 * radius;
                for (int i = radius + 1; i < data.Width - radius; i++, l += 3)
                {
                    long pixelSoma = integralImage[i + radius + ((j + radius) * width)];
                    long pixelSub1 = integralImage[i + radius + ((j - radius - 1) * width)];
                    long pixelSub2 = integralImage[i - radius - 1 + ((j + radius) * width)];
                    long pixelSoma2 = integralImage[i - radius - 1 + ((j - radius - 1) * width)];

                    byte r = (byte)((pixelSoma2 + pixelSoma - pixelSub1 - pixelSub2) / A);

                    l[0] = r;
                    l[1] = r;
                    l[2] = r;
                }
            });
        }
    }

    private static void setBuffer(BitmapData data)
        => integralImage = new long[3 * data.Width * data.Height];

    private static int[] applyBin2(BitmapData data, BitmapData bgData, float hparam = 0.05f)
    {
        float D = 10000;
        int histogramLen = (int)(1 / hparam) + 1;
        int[] hist = new int[histogramLen];
        int len = bgData.Height * bgData.Width;
        float medD = 0f;
        float sqSum = 0f;

        unsafe
        {
            byte* pDataBmp = (byte*)data.Scan0.ToPointer();
            byte* pDataBg = (byte*)bgData.Scan0.ToPointer();

            for (int j = 0; j < bgData.Height; j++)
            {
                byte* lBmp = pDataBmp + j * data.Stride;
                byte* lBkg = pDataBg + j * bgData.Stride;

                for (int i = 0; i < bgData.Width; i++, lBmp += 3, lBkg += 3)
                {
                    int db = lBkg[0] - lBmp[0];
                    int dg = lBkg[1] - lBmp[1];
                    int dr = lBkg[2] - lBmp[2];

                    float d = (dr * dr + db * db + dg * dg) / D;
                    if (d > 1f)
                        d = 1f;

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
            byte* pDataBmp = (byte*)data.Scan0.ToPointer();
            byte* pDataBg = (byte*)bgData.Scan0.ToPointer();

            Parallel.For(0, bgData.Height, j =>
            {
                byte* lBmp = pDataBmp + j * data.Stride;
                byte* lBkg = pDataBg + j * bgData.Stride;

                for (int i = 0; i < bgData.Width; i++, lBmp += 3, lBkg += 3)
                {
                    int db = lBkg[0] - lBmp[0];
                    int dg = lBkg[1] - lBmp[1];
                    int dr = lBkg[2] - lBmp[2];

                    float d = (dr * dr + db * db + dg * dg) / D;

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

        return hist;
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