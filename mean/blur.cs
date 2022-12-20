using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

public static class Blur
{
    private static long[] integralImage = null;

    public static Bitmap QuickParallelBlurGray(Bitmap bmp, int radius = 5) // Mais rápido
    {
        if (integralImage == null)
            SetBuffer(bmp);

        int N = radius * 2 + 1;
        int A = N * N;

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

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

            int width = bmp.Width;
            
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
                for (int i = 1; i < width; i++, l+=3)
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

            bmp.UnlockBits(data);

            return bmp;
        }
    }

    public static Bitmap QuickParallelBlur(Bitmap bmp, int radius = 5)
    {
        if (integralImage == null)
            SetBuffer(bmp);
        
        int N = radius * 2 + 1;
        int A = N * N;

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            int start = bmp.Width * bmp.Height;
            int width = bmp.Width;

            Parallel.For(0, 3, c =>
            {
                integralImage[c * start + 0] = p[c];

                for (int i = 1; i < width; i++)
                {
                    byte* l = p + 3 * i;
                    integralImage[c * start + i] = l[c] + integralImage[c * start + i - 1];
                }

                for (int j = 1; j < data.Height; j++)
                {
                    byte* l = p + j * data.Stride;
                    integralImage[c * start + j * width] = l[c] + integralImage[c * start + (j - 1) * width];
                }

                for (int j = 1; j < data.Height; j++)
                {
                    byte* l = p + j * data.Stride;
                    for (int i = 1; i < width; i++, l+=3)
                    {
                        integralImage[c * start + i + j * width] = l[c] 
                            + integralImage[c * start + i + (j - 1) * width]
                            + integralImage[c * start + i - 1 + j * width]
                            - integralImage[c * start + i - 1 + (j - 1) * width];
                    }
                }
            });

            Parallel.For(radius + 1, data.Height - radius, j =>
            {
                byte* l = p + j * data.Stride + 3 * radius;
                for (int i = radius + 1; i < data.Width - radius; i++, l+=3)
                {
                    long pixelSomaB = integralImage[i + radius + ((j + radius) * width)];
                    long pixelSub1B = integralImage[i + radius + ((j - radius - 1) * width)];
                    long pixelSub2B = integralImage[i - radius - 1 + ((j + radius) * width)];
                    long pixelSoma2B = integralImage[i - radius - 1 + ((j - radius - 1) * width)];

                    long pixelSomaG = integralImage[start + i + radius + ((j + radius) * width)];
                    long pixelSub1G = integralImage[start + i + radius + ((j - radius - 1) * width)];
                    long pixelSub2G = integralImage[start + i - radius - 1 + ((j + radius) * width)];
                    long pixelSoma2G = integralImage[start + i - radius - 1 + ((j - radius - 1) * width)];

                    long pixelSomaR = integralImage[2 * start + i + radius + ((j + radius) * width)];
                    long pixelSub1R = integralImage[2 * start + i + radius + ((j - radius - 1) * width)];
                    long pixelSub2R = integralImage[2 * start + i - radius - 1 + ((j + radius) * width)];
                    long pixelSoma2R = integralImage[2 * start + i - radius - 1 + ((j - radius - 1) * width)];

                    byte r = (byte)((pixelSoma2R + pixelSomaR - pixelSub1R - pixelSub2R) / A);
                    byte g = (byte)((pixelSoma2G + pixelSomaG - pixelSub1G - pixelSub2G) / A);
                    byte b = (byte)((pixelSoma2B + pixelSomaB - pixelSub1B - pixelSub2B) / A);

                    l[0] = b;
                    l[1] = g;
                    l[2] = r;
                }
            });

            bmp.UnlockBits(data);
            return bmp;
        }
    }

    public static void SetBuffer(Bitmap bmp)
    {
        integralImage = new long[3 * bmp.Width * bmp.Height];
    }
}