public static class Binarization
{
    public static Bitmap ApplyBin(Bitmap img, Bitmap bg, int treshold = 80)
    {   
        Bitmap returnBmp = new Bitmap(img.Width, img.Height);

        for (int i = 0; i < img.Width; i++)
        {
            for (int j = 0; j < img.Height; j++)
            {
                Color pixel = img.GetPixel(i,j);
                Color pixel2 = bg.GetPixel(i,j);

                int dr = pixel.R - pixel2.R;
                int dg = pixel.G - pixel2.G;
                int db = pixel.B - pixel2.B;
                int diff = (dr * dr + dg * dg + db * db) / 3;

                if (diff <= treshold)
                    returnBmp.SetPixel(i, j, Color.Black);
                else
                    returnBmp.SetPixel(i, j, Color.White);
            }
            
        }
        return returnBmp;       
    }    
}

