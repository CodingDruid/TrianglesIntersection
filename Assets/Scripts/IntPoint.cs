using UnityEngine;

/**
 * A data structure to manage points held by data (outlines, shapes, axes...) inside the grid
 * It approximates decimal values to int values (we do not need long value type as we won't use more than 1 or 2 significant figures for precision) 
 * **/
public struct IntPoint
{
    public const int DEFAULT_SCALE_PRECISION = 1000; //set the default scale value which is usually a power of 10 (a bigger value means bigger precision on data)

    private long m_x;
    public long X
    {
        get
        {
            return m_x;
        }
        set
        {
            m_x = value;
        }
    }
    private long m_y;
    public long Y
    {
        get
        {
            return m_y;
        }
        set
        {
            m_y = value;
        }
    }

    public static IntPoint zero
    {
        get
        {
            return new IntPoint(0, 0);
        }
    }

    public static IntPoint operator -(IntPoint a) { return new IntPoint(-a.X, -a.Y); }
    public static IntPoint operator -(IntPoint a, IntPoint b) { return new IntPoint(a.X - b.X, a.Y - b.Y); }
    public static bool operator !=(IntPoint lhs, IntPoint rhs) { return !lhs.Equals(rhs); }
    public static IntPoint operator *(float d, IntPoint a) { return new IntPoint(Mathf.RoundToInt(d * a.X), Mathf.RoundToInt(d * a.Y)); }
    public static IntPoint operator *(IntPoint a, float d) { return new IntPoint(Mathf.RoundToInt(a.X * d), Mathf.RoundToInt(a.Y * d)); }
    public static IntPoint operator /(IntPoint a, float d) { return new IntPoint(Mathf.RoundToInt(a.X / d), Mathf.RoundToInt(a.Y / d)); }
    public static IntPoint operator +(IntPoint a, IntPoint b) { return new IntPoint(a.X + b.X, a.Y + b.Y); }
    public static bool operator ==(IntPoint lhs, IntPoint rhs) { return lhs.Equals(rhs); }
    public static implicit operator Vector2(IntPoint a) { return new Vector2(a.X, a.Y); }

    public override bool Equals(object obj) 
    {
        if (!(obj is IntPoint))
            return false;

        return Equals((IntPoint)obj);
    }

    public bool Equals(IntPoint other)
    {
        if (X != other.X)
            return false;

        return Y == other.Y;
    }         

    public override int GetHashCode() { return base.GetHashCode(); }


    public long sqrMagnitude
    {
        get
        {
            return X * X + Y * Y;
        }
    }

    /***
     * Build a new IntPoint with x and y coordinates
     * **/
    public IntPoint(long x, long y) : this()
    {
        m_x = x;
        m_y = y;
    }

    public override string ToString()
    {
        return "(" + X + "," + Y + ")";
    }
}
