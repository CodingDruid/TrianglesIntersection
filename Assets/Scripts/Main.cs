using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public void Start()
    {
        float precision = 1E05F;

        Triangle t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 2) * precision;
        t1.m_points[1] = new IntPoint(4, -2) * precision;
        t1.m_points[2] = new IntPoint(1, 4) * precision;
        Triangle t2 = new Triangle();
        t2.m_points[0] = new IntPoint(0, 0) * precision;
        t2.m_points[1] = new IntPoint(4, 0) * precision;
        t2.m_points[2] = new IntPoint(3, 3) * precision;
        
        List<IntPoint> intersectionContour = t1.IntersectionWithTriangle(t2);
        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + (intersectionContour[i] / (float)precision));
        }


        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(9, -2) * precision;
        t1.m_points[2] = new IntPoint(5, 5) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(3, 2) * precision;
        t2.m_points[1] = new IntPoint(6, 0) * precision;
        t2.m_points[2] = new IntPoint(10, 5) * precision;
        
        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(0, 0) * precision;
        t2.m_points[1] = new IntPoint(9, -2) * precision;
        t2.m_points[2] = new IntPoint(5, 5) * precision;
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(3, 2) * precision;
        t1.m_points[1] = new IntPoint(6, 0) * precision;
        t1.m_points[2] = new IntPoint(10, 5) * precision;
        
        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }


        ///SEPARATION
        Debug.Log(">>>TEST1");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(3, 0) * precision;
        t1.m_points[2] = new IntPoint(2, 2) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(3, 2) * precision;
        t2.m_points[1] = new IntPoint(5, 1) * precision;
        t2.m_points[2] = new IntPoint(4, 3) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        if (intersectionContour == null)
            Debug.Log("contour is null");

        Debug.Log(">>>TEST2");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(6, 0) * precision;
        t1.m_points[2] = new IntPoint(3, 3) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(2, 1) * precision;
        t2.m_points[1] = new IntPoint(4, 1) * precision;
        t2.m_points[2] = new IntPoint(3, 2) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }


        Debug.Log(">>>TEST3");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(3, 0) * precision;
        t1.m_points[2] = new IntPoint(3, 3) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(0, 2) * precision;
        t2.m_points[1] = new IntPoint(5, 0) * precision;
        t2.m_points[2] = new IntPoint(4, 5) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        Debug.Log(">>>TEST4");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(6, 0) * precision;
        t1.m_points[2] = new IntPoint(3, 3) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(2, 1) * precision;
        t2.m_points[1] = new IntPoint(3, -2) * precision;
        t2.m_points[2] = new IntPoint(4, 1) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        Debug.Log(">>>TEST5");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(3, -1) * precision;
        t1.m_points[2] = new IntPoint(5, 3) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(2, 0) * precision;
        t2.m_points[1] = new IntPoint(3, -3) * precision;
        t2.m_points[2] = new IntPoint(4, 2) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        Debug.Log(">>>TEST6");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(4, -4) * precision;
        t1.m_points[2] = new IntPoint(1, 2) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(0, -3) * precision;
        t2.m_points[1] = new IntPoint(4, -2) * precision;
        t2.m_points[2] = new IntPoint(3, 1) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        Debug.Log(">>>TEST7");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(6, 2) * precision;
        t1.m_points[2] = new IntPoint(4, 5) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(0, 3) * precision;
        t2.m_points[1] = new IntPoint(3, -1) * precision;
        t2.m_points[2] = new IntPoint(4, 3) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        Debug.Log(">>>TEST8");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(3, -3) * precision;
        t1.m_points[2] = new IntPoint(6, 0) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(2, -1) * precision;
        t2.m_points[1] = new IntPoint(3, -5) * precision;
        t2.m_points[2] = new IntPoint(5, 1) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }

        Debug.Log(">>>TEST9");
        t1 = new Triangle();
        t1.m_points[0] = new IntPoint(0, 0) * precision;
        t1.m_points[1] = new IntPoint(6, -2) * precision;
        t1.m_points[2] = new IntPoint(5, 4) * precision;
        t2 = new Triangle();
        t2.m_points[0] = new IntPoint(2, 4) * precision;
        t2.m_points[1] = new IntPoint(2, -2) * precision;
        t2.m_points[2] = new IntPoint(6, 2) * precision;

        intersectionContour = t1.IntersectionWithTriangle(t2);

        for (int i = 0; i != intersectionContour.Count; i++)
        {
            Debug.Log("intersection[" + i + "] = " + intersectionContour[i]);
        }
    }
}