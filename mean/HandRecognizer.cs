using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

public class HandRecognizer
{   
    public Point getRightPixel(Bitmap bmp)
    {
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);
        
        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            
            for (int i = 50; i < bmp.Width - 50; i++)
            {
                for (int j = 50; j < bmp.Height - 50; j += 3)
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

    public Point getTopPixel(Bitmap bmp)
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
        if (topPixel.X < 45 || topPixel.Y < 45 || 
            topPixel.Y > bmp.Height - 45 ||
            topPixel.X  > bmp.Width - 45)
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

                for (int j = topPixel.Y - 45; j < topPixel.Y + 45; j += 5)
                {
                    byte* l = p + (topPixel.X - 45) * 3 + j * data.Stride;

                    for (int i = (topPixel.X - 45); i < (topPixel.X + 45); i += 5, l += (3*5))
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

    public Boolean FasterOpenHand(Bitmap bmp)
    {

        Point topPixel = getTopPixel(bmp);
        int calibration = 70;
        double whites = 0;
        double count = 0;
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);


        unsafe
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            
            for (int i = (topPixel.X - 50); i < (topPixel.X + 50); i++)
            {
                for (int j = topPixel.Y; j < (topPixel.Y + 50) ; j++)
                {
                    byte* l = p + ((j + 1) * data.Stride) - 3 * i;
                    if(l[0] > 1)
                    {
                        whites++;
                    }
                    count++;
                }
            }
        }

        bmp.UnlockBits(data);
        double temp1 = whites / count * 100.0;

        if (temp1 > calibration)
            return false;

        return true;

    }

    public (Boolean, double) FasterOpenHandV2(Bitmap bmp, int calibration, int area)
    {

        Point topPixel = getTopPixel(bmp);
        //int calibration = 70;
        double whites = 0;
        double count = 0;
        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite,
            PixelFormat.Format24bppRgb);

        if(topPixel.X > area+5 && topPixel.X < bmp.Width-(area+5))
        {

            unsafe
            {
                byte* p = (byte*)data.Scan0.ToPointer();
                
                for (int i = (topPixel.X - (int)area/4); i < (topPixel.X + area); i++)
                {
                    for (int j = topPixel.Y; j < (topPixel.Y + area) ; j++)
                    {
                        byte* l = p + ((j + 1) * data.Stride) - 3 * i;
                        if(l[0] > 1)
                        {
                            whites++;
                        }
                        count++;
                    }
                }
            }

            bmp.UnlockBits(data);

        }

        double temp1 = whites / count * 100.0;

        if (temp1 > calibration)
            return (false,temp1);

        return (true,temp1);

    }

    public (Boolean, double, double) SlowOpenHand(Bitmap bmp, int calibration, int area)
    {
        Point topPixel = getTopPixel(bmp);
        //int calibration = 70;
        double whites = 0;
        double colgate = 0;
        double count = 0;

        if(topPixel.X > area+5 && topPixel.X < bmp.Width-(area+5))
        {
            for (int j = topPixel.Y; j < (topPixel.Y + area*3/4) ; j++)
            {

                double comp = (j - topPixel.Y + 1) * (((1/area*3/4)-0.5)/5);

                for (int i = (topPixel.X - area/2); i < (topPixel.X + area*2/3); i++)
                {
                    Color pixel = bmp.GetPixel(i, j);

                    if (pixel.G != 0)
                    {
                        whites += comp;
                        colgate++;
                    }
                    count++;
                }
            }
        }

        double temp = whites/count * 100.0;

        if (colgate>calibration)
            return (false,whites,colgate);
        
        return (true,whites,colgate);
    }

    public (Boolean, double) BetterOpenHand(Bitmap bmp, int calibration=3600, int area=40)
    {
        Point topPixel = getTopPixel(bmp);
        double whites = 0;
        byte last = 0;
        int sequence = 1;

        if(topPixel.X > area+5 && topPixel.X < bmp.Width-(area+5))
        {
            for (int j = topPixel.Y; j < (topPixel.Y + area*3/4) ; j++)
            {
                sequence = 1;
                for (int i = (topPixel.X - area/2); i < (topPixel.X + area*2/3); i++)
                {
                    Color pixel = bmp.GetPixel(i, j);

                    if (pixel.G != 0)
                    {
                        whites += Math.Pow(sequence*2, 2);
                        if (last == 0)
                        {
                            sequence++;
                        }
                    }
                    else
                    {
                        last = 1;
                        sequence = 1;
                    }
                }
            }
        }

        if (whites>calibration)
            return (false, whites);
        
        return (true, whites);
    }
}
