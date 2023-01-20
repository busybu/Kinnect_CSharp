using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public static class Equalization
{
    public static void Equalize(Bitmap bmp,
        float threshold = 0.05f,
        float db = 0.05f)
    {
        (int[] R, int[] G, int[] B) histogram = hist(bmp, db);
        int len = bmp.Width * bmp.Height;
        int dropCount = (int)(len * threshold);

        float minR = 0;
        float minG = 0;
        float minB = 0;

        int dropedR = 0;
        int dropedG = 0;
        int dropedB = 0;
        for (int i = 0; i < histogram.R.Length; i++)
        {
            dropedR += histogram.R[i];

            if (dropedR > dropCount)
            {
                minR = i * db;
                break;
            }
        }
        for (int i = 0; i < histogram.G.Length; i++)
        {
            dropedG += histogram.G[i];

            if (dropedG > dropCount)
            {
                minG = i * db;
                break;
            }
        }
        for (int i = 0; i < histogram.B.Length; i++)
        {
            dropedB += histogram.B[i];

            if (dropedB > dropCount)
            {
                minB = i * db;
                break;
            }
        }

        float maxR = 0;
        float maxG = 0;
        float maxB = 0;

        dropedR = 0;
        dropedG = 0;
        dropedB = 0;
        for (int i = histogram.R.Length - 1; i >= 0; i--)
        {
            dropedR += histogram.R[i];

            if (dropedR > dropCount)
            {
                maxR = i * db;
                break;
            }
        }
        for (int i = histogram.G.Length - 1; i >= 0; i--)
        {
            dropedG += histogram.G[i];

            if (dropedG > dropCount)
            {
                maxG = i * db;
                break;
            }
        }
        for (int i = histogram.B.Length - 1; i >= 0; i--)
        {
            dropedB += histogram.B[i];

            if (dropedB > dropCount)
            {
                maxB = i * db;
                break;
            }
        }

        var fR = 1 / (maxR - minR);
        var fG = 1 / (maxG - minG);
        var fB = 1 / (maxB - minB);

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            Parallel.For(0, bmp.Height, j =>
            {
                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width; i++, line += 3)
                {
                    float b = line[0] / 255f;
                    float g = line[1] / 255f;
                    float r = line[2] / 255f;

                    float newB = (b - minB) * fB;
                    if (newB > 1f)
                        newB = 1f;
                    else if (newB < 0f)
                        newB = 0f;

                    line[0] = (byte)(255 * newB);

                    float newG = (g - minG) * fG;
                    if (newG > 1f)
                        newG = 1f;
                    else if (newG < 0f)
                        newG = 0f;

                    line[1] = (byte)(255 * newG);

                    float newR = (r - minR) * fR;
                    if (newR > 1f)
                        newR = 1f;
                    else if (newR < 0f)
                        newR = 0f;

                    line[2] = (byte)(255 * newR);
                }
            });
        }

        bmp.UnlockBits(data);

    }

    private static (int[] R, int[] G, int[] B) hist(Bitmap bmp, float db = 0.05f)
    {
        int histogramLen = (int)(1 / db) + 1;
        int[] histR = new int[histogramLen];
        int[] histG = new int[histogramLen];
        int[] histB = new int[histogramLen];

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            Parallel.For(0, bmp.Height, j =>
            {
                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width; i++, line += 3)
                {
                    byte b = line[0];
                    byte g = line[1];
                    byte r = line[2];

                    float pixelR = r / 255f;
                    float pixelG = g / 255f;
                    float pixelB = b / 255f;

                    histR[(int)(pixelR / db)]++;
                    histG[(int)(pixelG / db)]++;
                    histB[(int)(pixelB / db)]++;
                }
            });
        }

        bmp.UnlockBits(data);

        // foreach (var pixel in img)
        //     histR[(int)(pixel / db)]++;

        return (histR, histG, histB);
    }
    public static float GetParam(Bitmap bmp)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        long R = 0;
        long G = 0;
        long B = 0;
        long C = 0;
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            Parallel.For(0, bmp.Height, j =>
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

        bmp.UnlockBits(data);

        return (R + G + B) / (float)C;
    }

    public static void Normalize(Bitmap bmp, float param, float bgParam)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb
        );

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            float cal = bgParam / param;
            Parallel.For(0, bmp.Height, j =>
            {
                byte* line = p + j * data.Stride;

                for (int i = 0; i < data.Width - 1; i++, line += 3)
                {
                    line[0] = line[0] * cal > 255 ? (byte)255 : (byte)(line[0] * cal);
                    line[1] = line[1] * cal > 255 ? (byte)255 : (byte)(line[1] * cal);
                    line[2] = line[2] * cal > 255 ? (byte)255 : (byte)(line[2] * cal);

                }
            });
        }
        
        bmp.UnlockBits(data);
    }

    // private static void otsu((Bitmap bmp, float[] img) t, float db = 0.05f)
    // {
    //     var histogram = hist(t.bmp, db);
    //     int treshold = 0;


    //     float Ex0 = 0;
    //     float Ex1 = t.img.Average();
    //     float Dx0 = 0;
    //     float Dx1 = t.img.Sum(x => x * x);
    //     int N0 = 0;
    //     int N1 = t.img.Length;

    //     float minstddev = float.PositiveInfinity;

    //     for (int i = 0; i < histogram.R.Length; i++)
    //     {
    //         float value = db * (2 * i + 1) / 2;
    //         float s = histogram.R[i] * value;

    //         if (N0 == 0 && histogram.R[i] == 0)
    //             continue;

    //         Ex0 = (Ex0 * N0 + s) / (N0 + histogram.R[i]);
    //         Ex1 = (Ex1 * N1 - s) / (N1 - histogram.R[i]);

    //         N0 += histogram.R[i];
    //         N1 -= histogram.R[i];

    //         Dx0 += value * value * histogram.R[i];
    //         Dx1 -= value * value * histogram.R[i];

    //         float stddev =
    //             Dx0 - N0 * Ex0 * Ex0 +
    //             Dx1 - N1 * Ex1 * Ex1;

    //         if (float.IsInfinity(stddev) ||
    //             float.IsNaN(stddev))
    //             continue;

    //         if (stddev < minstddev)
    //         {
    //             minstddev = stddev;
    //             threshold = i;
    //         }
    //     }
    //     float bestTreshold = db * (2 * threshold + 1) / 2;

    //     tresh(t, bestTreshold);
    // }
}
