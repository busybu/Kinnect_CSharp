using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

public class HandRecognizer
{   
    private Point getRightPixel(Bitmap bmp)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            
            for (int i = 1; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j += 3)
                {
                    byte* l = p + ((j + 1) * data.Stride) - 3 * i;
                    if(l[0] > 1)
                    {
                        bmp.UnlockBits(data);
                        return new Point(bmp.Width - i, j);
                    }
                }
            }
        }
        bmp.UnlockBits(data);
        return Point.Empty;
    }

    private Point getTopPixel(Bitmap bmp)
    {
        Point rightPixel = getRightPixel(bmp);

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            for (int j = 1; j < bmp.Height; j += 3)
            {
                byte* l = p + (rightPixel.X - 80) * 3 + j * data.Stride;
                for (int i = (rightPixel.X - 80); i < (rightPixel.X + 80); i += 3, l += (3*3))
                {
                    if (l[0] > 0)
                    {
                        bmp.UnlockBits(data);
                        return new Point(i,j);
                    }
                }
            }
        }
        bmp.UnlockBits(data);
        return Point.Empty;
    }

    public Point GetCenterPixel(Bitmap bmp)
    {
        Point topPixel = getTopPixel(bmp);
        if (topPixel.X < 100 || topPixel.Y < 100 || 
            topPixel.Y > bmp.Height - 100 ||
            topPixel.X  > bmp.Width - 100)
            return Point.Empty;

        var centerPixel = new Point();

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();

            for (int k = 0; k < 3; k++)
            {
                long sX = 0;
                long sY = 0;
                int count = 0;

                for (int j = topPixel.Y - 100; j < topPixel.Y + 100; j += 5)
                {
                    byte* l = p + (topPixel.X - 100) * 3 + j * data.Stride;

                    for (int i = (topPixel.X - 100); i < (topPixel.X + 100); i += 5, l += (3*5))
                    {
                        if (l[0] != 0)
                        {
                            sX += i;
                            sY += j;
                            count++;
                        }
                    }
                }
                
                centerPixel = new Point((int)(sX / count), (int)(sY / count));
            }
            bmp.UnlockBits(data);
        }
        
        return centerPixel;
    }

    public Boolean IsOpenHand(Bitmap bmp)
    {
        Point topPixel = getTopPixel(bmp);
        int calibration = 70;
        double whites = 0;
        double count = 0;
        for (int j = topPixel.Y; j < (topPixel.Y + 50) ; j++)
            for (int i = (topPixel.X - 50); i < (topPixel.X + 50); i++)
            {
                Color pixel = bmp.GetPixel(i, j);

                if (pixel.G != 0)
                    whites++;
                count++;
            }

        double temp = whites/count*100.0;

        if (temp>calibration)
            return false;
        
        return true;
    }
}