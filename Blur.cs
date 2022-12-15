public static class Blur
{
    public static Bitmap ApplyBlur(Bitmap bmp, int radius = 5)
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

    public static Bitmap ApplyFastBlurGray(Bitmap bmp, int radius = 5)
    {
        Bitmap blurred = new Bitmap(bmp.Width, bmp.Height);
        long[] integralImage = new long[bmp.Width * bmp.Height];
        int N = radius * 2 + 1;
        int A = N * N;

        integralImage[0] = (long)((bmp.GetPixel(0,0).R * 0.3 + bmp.GetPixel(0,0).G * 0.59 + bmp.GetPixel(0,0).B * 0.11));
        integralImage[1] = (long)((bmp.GetPixel(0,1).R * 0.3 + bmp.GetPixel(0,1).G * 0.59 + bmp.GetPixel(0,1).B * 0.11)) + integralImage[0];
        Color x = bmp.GetPixel(1,0);
        integralImage[bmp.Width + 1] = (long)(((x.R * 0.3 + x.G  * 0.59 + x.B * 0.11 ) / 3) + integralImage[1]);

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
            for (int j = radius; j < bmp.Height; j++)
            {
                for (int i = radius; i < bmp.Width; i++)
                {
                    
                    long pixelAtual = integralImage[i + (j * bmp.Width)];
                    long pixelSub1 = integralImage[i - radius + (j * bmp.Width)];
                    long pixelSub2 = integralImage[i + ((j - radius)* bmp.Width)];
                    long pixelSoma = integralImage[i - radius + ((j - radius) * bmp.Width)];
                    
                    int r = (int)(pixelAtual + pixelSoma - pixelSub1 - pixelSub2) / A;
                    

                        blurred.SetPixel(i, j, Color.FromArgb(r,r,r));
                }
            }

            for (int i = 0; i < radius; i++)
                for (int j = 0; j < radius; j++)
                    blurred.SetPixel(i,j, Color.White);

            return blurred;
        
    }

    public static Bitmap ApplyFastBlur(Bitmap bmp, int radius = 5)
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
        for (int j = radius; j < bmp.Height; j++)
        {
            for (int i = radius; i < bmp.Width; i++)
            {
                
                long pixelAtualR = integralImageR[i + (j * bmp.Width)];
                long pixelSub1R = integralImageR[i - radius + (j * bmp.Width)];
                long pixelSub2R = integralImageR[i + ((j - radius)* bmp.Width)];
                long pixelSomaR = integralImageR[i - radius + ((j - radius) * bmp.Width)];
                
                int r = (int)(pixelAtualR + pixelSomaR - pixelSub1R - pixelSub2R) / A;
               
                long pixelAtualG = integralImageG[i + (j * bmp.Width)];
                long pixelSub1G = integralImageG[i - radius + (j * bmp.Width)];
                long pixelSub2G = integralImageG[i + ((j - radius)* bmp.Width)];
                long pixelSomaG = integralImageG[i - radius + ((j - radius) * bmp.Width)];
                
                int g = (int)(pixelAtualG + pixelSomaG - pixelSub1G - pixelSub2G) / A;
                
                long pixelAtualB = integralImageB[i + (j * bmp.Width)];
                long pixelSub1B = integralImageB[i - radius + (j * bmp.Width)];
                long pixelSub2B = integralImageB[i + ((j - radius)* bmp.Width)];
                long pixelSomaB = integralImageB[i - radius + ((j - radius) * bmp.Width)];
                
                int b = (int)(pixelAtualB + pixelSomaB - pixelSub1B - pixelSub2B) / A;

                blurred.SetPixel(i, j, Color.FromArgb(r,g,b));
            }
        }

        for (int i = 0; i < radius; i++)
            for (int j = 0; j < radius; j++)
                blurred.SetPixel(i,j, Color.White);

        return blurred;
    }
}