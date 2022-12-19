using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
public static class Blur
{
    public static Bitmap SlowBlur(Bitmap bmp, int radius = 5)
    {
        Bitmap blurred = new Bitmap(bmp.Width, bmp.Height);
        int N = radius * 2 + 1;
        int A = N * N;

        for (int j = radius; j < bmp.Height - radius; j++)
        {
            for (int i = radius; i < bmp.Width - radius; i++)
            {
                long sR = 0;
                long sG = 0;
                long sB = 0;
                for (int k = -radius; k < radius; k++)
                {
                    for (int l = -radius; l < radius; l++)
                    {
                        var c = bmp.GetPixel(i + k, j + l);
                        sR += c.R;
                        sB += c.B;
                        sG += c.G;
                    }
                }
                int newR = (int)(sR / A);
                int newG = (int)(sG / A);
                int newB = (int)(sB / A);

                blurred.SetPixel(i, j, Color.FromArgb(newR, newG, newB));
            }
        }

        return blurred;
    }

    public static Bitmap NormalBlurGray(Bitmap bmp, int radius = 5)
    {
        Bitmap blurred = new Bitmap(bmp.Width, bmp.Height);
        long[] integralImage = new long[bmp.Width * bmp.Height];
        int N = radius * 2 + 1;
        int A = N * N;

        integralImage[0] = (long)((bmp.GetPixel(0,0).R * 0.3 + bmp.GetPixel(0,0).G * 0.59 + bmp.GetPixel(0,0).B * 0.11));
        integralImage[1] = (long)((bmp.GetPixel(0,1).R * 0.3 + bmp.GetPixel(0,1).G * 0.59 + bmp.GetPixel(0,1).B * 0.11)) + integralImage[0];
        Color x = bmp.GetPixel(1,0);
        integralImage[bmp.Width + 1] = (long)(((x.R * 0.3 + x.G  * 0.59 + x.B * 0.11 )) + integralImage[1]);

        // Calculate integral image
        for (int j = 1; j < bmp.Height; j++)
        {
            for (int i = 1; i < bmp.Width; i++)
            {
                var c = bmp.GetPixel(i, j);
                long r = c.R;
                long g = c.G;
                long b = c.B;

                if (i > 0)
                {
                    r += integralImage[i - 1 + j * bmp.Width];
                    g += integralImage[i - 1 + j * bmp.Width];
                    b += integralImage[i - 1 + j * bmp.Width];
                }
                if (j > 0)
                {
                    r += integralImage[i + (j - 1) * bmp.Width];
                    g += integralImage[i + (j - 1) * bmp.Width];
                    b += integralImage[i + (j - 1) * bmp.Width];
                }
                if (i > 0 && j > 0)
                {
                    r -= integralImage[i - 1 + (j - 1) * bmp.Width];
                    g -= integralImage[i - 1 + (j - 1) * bmp.Width];
                    b -= integralImage[i - 1 + (j - 1) * bmp.Width];
                }

                integralImage[i + j * bmp.Width] = (long)((r*0.3 + g*0.59 + b*0.11));
            }
        }
            // Calculate blurred image
            for (int j = radius + 1; j < bmp.Height - radius; j++)
            {
                for (int i = radius + 1; i < bmp.Width - radius; i++)
                {
                    long pixelSoma = integralImage[i + radius + ((j + radius) * bmp.Width)];
                    long pixelSub1 = integralImage[i + radius + ((j - radius - 1) * bmp.Width)];
                    long pixelSub2 = integralImage[i - radius - 1 + ((j + radius) * bmp.Width)];
                    long pixelSoma2 = integralImage[i - radius - 1 + ((j - radius - 1) * bmp.Width)];
                    
                    int r = (int)(pixelSoma2 + pixelSoma - pixelSub1 - pixelSub2) / A;
                    

                    blurred.SetPixel(i, j, Color.FromArgb(r,r,r));
                }
            }

            for (int i = 0; i < radius; i++)
                for (int j = 0; j < radius; j++)
                    blurred.SetPixel(i,j, Color.White);

            return blurred;
        
    }

    public static Bitmap NormalBlur(Bitmap bmp, int radius = 5)
    {
        Bitmap blurred = new Bitmap(bmp.Width, bmp.Height);
        long[] integralImageR = new long[bmp.Width * bmp.Height];
        long[] integralImageG = new long[bmp.Width * bmp.Height];
        long[] integralImageB = new long[bmp.Width * bmp.Height];
        int N = radius * 2 + 1;
        int A = N * N;

        integralImageR[0] = (long)((bmp.GetPixel(0,0).R));
        integralImageR[1] = (long)((bmp.GetPixel(0,1).R)) + integralImageR[0];
        Color x = bmp.GetPixel(1,0);
        integralImageR[bmp.Width + 1] = (long)(x.R + integralImageR[1]);

        integralImageG[0] = (long)((bmp.GetPixel(0,0).G));
        integralImageG[1] = (long)((bmp.GetPixel(0,1).G)) + integralImageG[0];
        Color y = bmp.GetPixel(1,0);
        integralImageG[bmp.Width + 1] = (long)(y.G + integralImageG[1]);

        integralImageB[0] = (long)((bmp.GetPixel(0,0).B));
        integralImageB[1] = (long)((bmp.GetPixel(0,1).B)) + integralImageB[0];
        Color z = bmp.GetPixel(1,0);
        integralImageB[bmp.Width + 1] = (long)(z.B + integralImageB[1]);

        // Calculate integral image
        for (int j = 1; j < bmp.Height; j++)
        {
            for (int i = 1; i < bmp.Width; i++)
            {
                var c = bmp.GetPixel(i, j);
                long r = c.R;
                long g = c.G;
                long b = c.B;

                if (i > 0)
                {
                    r += integralImageR[i - 1 + j * bmp.Width];
                    g += integralImageG[i - 1 + j * bmp.Width];
                    b += integralImageB[i - 1 + j * bmp.Width];
                }
                if (j > 0)
                {
                    r += integralImageR[i + (j - 1) * bmp.Width];
                    g += integralImageG[i + (j - 1) * bmp.Width];
                    b += integralImageB[i + (j - 1) * bmp.Width];
                }
                if (i > 0 && j > 0)
                {
                    r -= integralImageR[i - 1 + (j - 1) * bmp.Width];
                    g -= integralImageG[i - 1 + (j - 1) * bmp.Width];
                    b -= integralImageB[i - 1 + (j - 1) * bmp.Width];
                }

                integralImageR[i + j * bmp.Width] = (long)(r);
                integralImageG[i + j * bmp.Width] = (long)(g);
                integralImageB[i + j * bmp.Width] = (long)(b);
            }
        }
        
        // Calculate blurred image
        for (int j = radius + 1; j < bmp.Height - radius; j++)
        {
            for (int i = radius + 1; i < bmp.Width - radius; i++)
            {
                
                long pixelSomaR = integralImageR[i + radius + ((j + radius) * bmp.Width)];
                long pixelSub1R = integralImageR[i + radius + ((j - radius - 1) * bmp.Width)];
                long pixelSub2R = integralImageR[i - radius - 1 + ((j + radius) * bmp.Width)];
                long pixelSoma2R = integralImageR[i - radius - 1 + ((j - radius - 1) * bmp.Width)];
                
                
                int r = (int)(pixelSoma2R + pixelSomaR - pixelSub1R - pixelSub2R) / A;
               
                long pixelSomaG = integralImageG[i + radius + ((j + radius) * bmp.Width)];
                long pixelSub1G = integralImageG[i + radius + ((j - radius - 1) * bmp.Width)];
                long pixelSub2G = integralImageG[i - radius - 1 + ((j + radius) * bmp.Width)];
                long pixelSoma2G = integralImageG[i - radius - 1 + ((j - radius - 1) * bmp.Width)];
                
                int g = (int)(pixelSoma2G + pixelSomaG - pixelSub1G - pixelSub2G) / A;
                
                long pixelSomaB = integralImageB[i + radius + ((j + radius) * bmp.Width)];
                long pixelSub1B = integralImageB[i + radius + ((j - radius - 1) * bmp.Width)];
                long pixelSub2B = integralImageB[i - radius - 1 + ((j + radius) * bmp.Width)];
                long pixelSoma2B = integralImageB[i - radius - 1 + ((j - radius - 1) * bmp.Width)];
                
                int b = (int)(pixelSoma2B + pixelSomaB - pixelSub1B - pixelSub2B) / A;

                if (b > 255)
                    b = 255;
                blurred.SetPixel(i, j, Color.FromArgb(r,g,b));
            }
        }

        for (int i = 0; i < radius; i++)
            for (int j = 0; j < radius; j++)
                blurred.SetPixel(i,j, Color.White);

        return blurred;
    }

    public static Bitmap QuickBlurGray(Bitmap bmp, int radius = 5)
    {

        long[] integralImage = new long[bmp.Width * bmp.Height];
        int N = radius * 2 + 1;
        int A = N * N;

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            //bmp[0,0]
            byte* l = p;
            integralImage[0] = (long)((l[2] * 0.3 + l[1] * 0.59 + l[0] * 0.11));
            
            //bmp[0,1]
            l = p + 3;
            integralImage[1] = (long)((l[2] * 0.3 + l[1] * 0.59 + l[0] * 0.11)) + integralImage[0];
            
            // data[1,0]
            l = p + data.Stride;
            integralImage[bmp.Width + 1] = (long)((((l[2]) * 0.3 + (l[1]) * 0.59 +  (l[0]) * 0.11 )) + integralImage[1]);

            // for
            for (int j = 1; j < data.Height; j++)
            {
                l = p + j * data.Stride;
                for (int i = 1; i < data.Width; i++, l+=3)
                {
                    long b = l[0];
                    long g = l[1];
                    long r = l[2];
                    
                    if (i > 0)
                    {
                        r += integralImage[i - 1 + j * bmp.Width];
                        g += integralImage[i - 1 + j * bmp.Width];
                        b += integralImage[i - 1 + j * bmp.Width];
                    }
                    if (j > 0)
                    {
                        r += integralImage[i + (j - 1) * bmp.Width];
                        g += integralImage[i + (j - 1) * bmp.Width];
                        b += integralImage[i + (j - 1) * bmp.Width];
                    }
                    if (i > 0 && j > 0)
                    {
                        r -= integralImage[i - 1 + (j - 1) * bmp.Width];
                        g -= integralImage[i - 1 + (j - 1) * bmp.Width];
                        b -= integralImage[i - 1 + (j - 1) * bmp.Width];
                    }
                
                    integralImage[i + j * bmp.Width] = (long)((r * 0.3 + g * 0.59 + b * 0.11));
                }
            }

            for (int j = radius + 1; j < data.Height - radius; j++)
            {
                l = p + j * data.Stride;
                for (int i = radius + 1; i < data.Width - radius; i++, l+=3)
                {
                    long pixelSoma = integralImage[i + radius + ((j + radius) * bmp.Width)];
                    long pixelSub1 = integralImage[i + radius + ((j - radius - 1) * bmp.Width)];
                    long pixelSub2 = integralImage[i - radius - 1 + ((j + radius) * bmp.Width)];
                    long pixelSoma2 = integralImage[i - radius - 1 + ((j - radius - 1) * bmp.Width)];

                    byte r = (byte)((pixelSoma2 + pixelSoma - pixelSub1 - pixelSub2) / A);

                    l[0] = r;
                    l[1] = r;
                    l[2] = r;
                }
            }

            bmp.UnlockBits(data);
            return bmp;
        }

    }

    public static Bitmap QuickBlur(Bitmap bmp, int radius = 5)
    {
        long[] integralImageR = new long[bmp.Width * bmp.Height];
        long[] integralImageG = new long[bmp.Width * bmp.Height];
        long[] integralImageB = new long[bmp.Width * bmp.Height];

        int N = radius * 2 + 1;
        int A = N * N;

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            //bmp[0,0]
            byte* l = p;
            integralImageR[0] = (long)((l[2]));
            integralImageG[0] = (long)((l[1]));
            integralImageB[0] = (long)((l[0]));
            
            //bmp[0,1]
            l = p + 3;
            integralImageR[1] = (long)(l[2]) + integralImageR[0];
            integralImageG[1] = (long)(l[1]) + integralImageG[0];
            integralImageB[1] = (long)(l[0]) + integralImageB[0];
            
            // data[1,0]
            l = p + data.Stride;
            integralImageR[bmp.Width + 1] = (long)(l[2])+ integralImageR[1];
            integralImageG[bmp.Width + 1] = (long)(l[1])+ integralImageG[1];
            integralImageB[bmp.Width + 1] = (long)(l[0])+ integralImageB[1];

            // for
            for (int j = 1; j < data.Height; j++)
            {
                l = p + j * data.Stride;
                for (int i = 1; i < data.Width; i++, l+=3)
                {
                    long b = l[0];
                    long g = l[1];
                    long r = l[2];
                    
                    if (i > 0)
                    {
                        r += integralImageR[i - 1 + j * bmp.Width];
                        g += integralImageG[i - 1 + j * bmp.Width];
                        b += integralImageB[i - 1 + j * bmp.Width];
                    }
                    if (j > 0)
                    {
                        r += integralImageR[i + (j - 1) * bmp.Width];
                        g += integralImageG[i + (j - 1) * bmp.Width];
                        b += integralImageB[i + (j - 1) * bmp.Width];
                    }
                    if (i > 0 && j > 0)
                    {
                        r -= integralImageR[i - 1 + (j - 1) * bmp.Width];
                        g -= integralImageG[i - 1 + (j - 1) * bmp.Width];
                        b -= integralImageB[i - 1 + (j - 1) * bmp.Width];
                    }
                
                    integralImageR[i + j * bmp.Width] = (long)(r);
                    integralImageG[i + j * bmp.Width] = (long)(g);
                    integralImageB[i + j * bmp.Width] = (long)(b);
                }
            }

            for (int j = radius + 1; j < data.Height - radius; j++)
            {
                l = p + j * data.Stride;
                for (int i = radius + 1; i < data.Width - radius; i++, l+=3)
                {
                    long pixelSomaR = integralImageR[i + radius + ((j + radius) * bmp.Width)];
                    long pixelSub1R = integralImageR[i + radius + ((j - radius - 1) * bmp.Width)];
                    long pixelSub2R = integralImageR[i - radius - 1 + ((j + radius) * bmp.Width)];
                    long pixelSoma2R = integralImageR[i - radius - 1 + ((j - radius - 1) * bmp.Width)];

                    long pixelSomaB = integralImageB[i + radius + ((j + radius) * bmp.Width)];
                    long pixelSub1B = integralImageB[i + radius + ((j - radius - 1) * bmp.Width)];
                    long pixelSub2B = integralImageB[i - radius - 1 + ((j + radius) * bmp.Width)];
                    long pixelSoma2B = integralImageB[i - radius - 1 + ((j - radius - 1) * bmp.Width)];

                    long pixelSomaG = integralImageG[i + radius + ((j + radius) * bmp.Width)];
                    long pixelSub1G = integralImageG[i + radius + ((j - radius - 1) * bmp.Width)];
                    long pixelSub2G = integralImageG[i - radius - 1 + ((j + radius) * bmp.Width)];
                    long pixelSoma2G = integralImageG[i - radius - 1 + ((j - radius - 1) * bmp.Width)];

                    byte r = (byte)((pixelSoma2R + pixelSomaR - pixelSub1R - pixelSub2R) / A);
                    byte g = (byte)((pixelSoma2G + pixelSomaG - pixelSub1G - pixelSub2G) / A);
                    byte b = (byte)((pixelSoma2B + pixelSomaB - pixelSub1B - pixelSub2B) / A);



                    l[0] = b;
                    l[1] = g;
                    l[2] = r;
                }
            }

            bmp.UnlockBits(data);
            return bmp;
        }
         
    }

    public static Bitmap QuickParallelBlurGray(Bitmap bmp, int radius = 5)
    {
        throw new NotImplementedException();
    }

    public static Bitmap QuickParallelBlur(Bitmap bmp, int radius = 5)
    {
        throw new NotImplementedException();
    }
}