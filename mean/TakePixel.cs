public class TakePixel
{
    public bool Detect(Point point, Point last, int Calibration){
        var DistanciaDoUltimo = Math.Sqrt(Math.Pow(Math.Abs(last.X - point.X),2) + Math.Pow(Math.Abs(last.Y - point.Y),2));
        
        if (DistanciaDoUltimo > Calibration)
        {
            return true;
        }
        return false;
    }
}
