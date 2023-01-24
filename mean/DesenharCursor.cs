public class PointsHandler
{
  private List<Point> list = new List<Point>();
  Bitmap crr = new Bitmap(1200, 1200);
  Graphics g = null;

  public PointsHandler()
  {
    g = Graphics.FromImage(crr);
  }

  public void RegisterCursor(Point cursor, bool isDown)
  {
    if (isDown)
      list.Add(cursor);
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
    
    return data;
  }
  public void Clear()
    => list.Clear();
}