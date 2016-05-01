using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Geometry
{
    static public long Determinant(IntPoint u, IntPoint v, IntPoint w)
    {
        IntPoint vec1 = u - w;
        IntPoint vec2 = v - w;

        return Determinant(vec1, vec2);
    }

    static public long Determinant(IntPoint u, IntPoint v)
    {
        return u.X * v.Y - u.Y * v.X;
    }

    static public long DotProduct(IntPoint u, IntPoint v)
    {
        return u.X * v.X + u.Y * v.Y;
    }
}
