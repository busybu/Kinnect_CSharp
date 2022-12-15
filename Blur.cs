public static class Blur
{
    public static Bitmap ApplyBlur(Bitmap bmp, int radius = 5)
    {
        Bitmap blurred = new Bitmap(bmp.Width, bmp.Height);
        int N = radius * 2 + 1;
        int A = N * N;

        for (int i = radius; i < bmp.Width - radius; i++)
        {
            for (int j = radius; j < bmp.Height - radius; j++)
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
}