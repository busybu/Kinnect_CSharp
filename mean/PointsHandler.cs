public class PointsHandler
{
  private List<Point> list = new List<Point>();
  Bitmap crr = new Bitmap(1200, 1200);
  Graphics g = null;
  TakePixel tp = new TakePixel();
  Point last = new Point();

  public PointsHandler()
  {
    g = Graphics.FromImage(crr);
  }
  public void RegisterCursor(Point cursor, bool isClosed, Graphics g_aparente, int distanciaTela)
  {
    if (isClosed)
    {
      g_aparente.DrawString(cursor.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 100));
        g_aparente.DrawString(last.ToString(), SystemFonts.CaptionFont,
        Brushes.White, new PointF(10, 110));
      if(tp.Detect(cursor, last, 5, distanciaTela / 4))
      {
        list.Add(cursor);
        last = cursor;
        if (list.Count > 1)
          g_aparente.DrawLines(new Pen(Brushes.Black, 10f), list.ToArray());
      }
      else
      {
        list.Add(last);
      }
    }
    else
    {
      GenerateMnist();
      Clear();
    }
  }
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
      new Rectangle(0,0,result.Width,result.Height),
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
        for(int i = 0; i < result.Width; i++, linha+=3)
        {
          data[index] = (byte)(255 - linha[0]);
          index++;
        }
      }
    }
    return data;
  }
  public void Clear() => list.Clear();

}
