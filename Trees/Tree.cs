using System;
using System.Linq;

public partial class NumberModel
{
    public int Choose(int[] data)
    {
        var funcArr = new Func<int[], float>[]
        {
            Choose0,
            Choose1,
            Choose2,
            Choose3,
            Choose4,
            Choose5,
            Choose6,
            Choose7,
            Choose8,
            Choose9
        };
        return funcArr.Select((f, i) => (i, f))
                .MaxBy(t => t.f(data)).i;
    }
}