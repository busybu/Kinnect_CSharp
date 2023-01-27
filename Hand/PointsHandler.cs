using RandomFlorestLib;

public class PointsHandler
{
  public static List<string> Messages { get; private set; } = new List<string>();
  public static Bitmap LastNumber { get; private set; }

  private List<Point> list = new List<Point>();
  Bitmap crr = new Bitmap(1200, 1200);
  Graphics g = null;
  TakePixel tp = new TakePixel();
  Point last = new Point();

  public PointsHandler()
  {
    g = Graphics.FromImage(crr);
  }

  int count = 0;
  bool state = false;
  public void RegisterCursor(Point cursor, bool isClosed, Graphics g_aparente, int distanciaTela, RandomFlorest rfr)
  {
    if (isClosed != state)
    {
      count++;
      if (count > 5)
      {
        count = 0;
        state = !state;
      }
    }
    else count = 0;

    if (state)
    {
      g_aparente.DrawString(cursor.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 100));
        g_aparente.DrawString(last.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 110));
      if(tp.Detect(cursor, last, 5, distanciaTela / 4) && (cursor.X != 0 && cursor.Y != 0))
      {
        list.Add(cursor);
        last = cursor;
        if (list.Count > 1)
          g_aparente.DrawLines(new Pen(Brushes.Black, 10f), list.ToArray());
      }
    }
    else
    {
      byte[] Mnist = GenerateMnist();
      if (Mnist.Any(x => x > 0) && Mnist.All(x => x == 255 || x == 0))
        return;
      Mnist = Mnist.Select(x => x == (byte)255 ? (byte)0 : x).ToArray();
      Mnist = Mnist.Select(x => x == (byte)1 ? (byte)0 : x).ToArray();
      
      byte[] processedMnist = Convolutions.Core.Convolutions.Convolute4(Mnist, 28, 28);

      if (processedMnist.All(x => x == 0))
      {
        Clear();
        return; 
      }

      File.AppendAllLines("output.ds", new string[] { Mnist.Append((byte)(i % 10)).Aggregate("", (a, b) => a + b.ToString() + ",") });
      i++;

      int numero = rfr.Choose(processedMnist);
      Messages.Add(numero.ToString());
      
      Clear();
    }
  }
  int i = 0;

  public byte[] GenerateMnist(int margin = 100)
  {
    var data = new byte[28 * 28];

    Pen pen = new Pen(Color.Black, 20f);
    g.Clear(Color.White);
    if (list.Count > 2)
      g.DrawLines(pen, list.ToArray());

    if (list.Count() == 0)
      return data;
    var x0 = list.Min(p => p.X) - margin;
    var y0 = list.Min(p => p.Y) - margin;

    var x = list.Max(p => p.X) + margin;
    var y = list.Max(p => p.Y) + margin;

    var wid = x - x0;  
    var hei = y - y0;

    Rectangle ret = new Rectangle(x0, y0, wid, hei);
    var result = new Bitmap(28, 28);
    var rg = Graphics.FromImage(result);
    rg.DrawImage(crr, new Rectangle(0, 0, 28, 28),
      ret, GraphicsUnit.Pixel);
    
    var sla = result.LockBits(
      new Rectangle(0, 0, result.Width, result.Height),
      System.Drawing.Imaging.ImageLockMode.ReadOnly,
      System.Drawing.Imaging.PixelFormat.Format24bppRgb
    );

    int index = 0;

    unsafe
    {
      byte* p = (byte*)(sla.Scan0.ToPointer());
      for(int j = 0; j < result.Height; j++)
      {
        byte* linha = p + j * sla.Stride;
        for(int i = 0; i < result.Width; i++, linha += 3)
        {
          data[index] = (byte)(255 - linha[0]);
          index++;
        }
      }
    }
    LastNumber = (Bitmap)result.Clone();
    return data;
  }
  public void Clear() => list.Clear();

}
