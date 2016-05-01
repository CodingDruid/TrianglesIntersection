using UnityEngine;
using System.Collections.Generic;

/**
 * Class to hold data for a triangle edge (i.e 2 IntPoint)
 * **/
public class Edge
{
    public IntPoint m_pointA { get; set; }
    public IntPoint m_pointB { get; set; }

    public Edge(IntPoint pointA, IntPoint pointB)
    {
        m_pointA = pointA;
        m_pointB = pointB;
    }

    public bool Equals(Edge other)
    {
        return (this.m_pointA == other.m_pointA && this.m_pointB == other.m_pointB)
               ||
               (this.m_pointA == other.m_pointB && this.m_pointA == other.m_pointB);
    }

    /**
     * Return the intersection (if it exists) between this edge and the line defined by linePoint and lineDirection
     * **/
    public void IntersectionWithLine(IntPoint linePoint, IntPoint lineDirection, out bool intersects, out IntPoint intersection)
    {
        //Store global values in temporary values for the purpose of this calculation
        IntPoint pointA = m_pointA;
        IntPoint pointB = m_pointB;

        //Both lines equation
        double x, y;
        if (lineDirection.X != 0 && pointA.X != pointB.X) //y = a1x + b1 && y = a2x + b2
        {
            double a1 = lineDirection.Y / (double) lineDirection.X;
            double b1 = linePoint.Y - a1 * linePoint.X;
            double a2 = (pointB.Y - pointA.Y) / (double) (pointB.X - pointA.X);
            double b2 = pointA.Y - a2 * pointA.X;

            if (a1 == a2) //parallel lines
            {
                intersects = false;
                intersection = IntPoint.zero;
                return;
            }
            else
            {
                x = (b2 - b1) / (a1 - a2);
                y = a1 * x + b1;
            }
        }
        else if (lineDirection.X != 0 && pointA.X == pointB.X) //y = a1x + b1 && x = a2
        {
            double a1 = lineDirection.Y / (double) lineDirection.X;
            double b1 = linePoint.Y - a1 * linePoint.X;
            double a2 = pointA.X;

            x = a2;
            y = a1 * a2 + b1;
        }
        else if (lineDirection.X == 0 && pointA.X != pointB.X) //x = a1 && y = a2x + b2
        {
            double a1 = linePoint.X;
            double a2 = (pointB.Y - pointA.Y) / (float) (pointB.X - pointA.X);
            double b2 = pointA.Y - a2 * pointA.X;

            x = a1;
            y = a2 * a1 + b2;
        }
        else //parallel vertical lines, no intersection or infinite intersections. In both cases return no intersection
        {
            intersects = false;
            intersection = IntPoint.zero;
            return;
        }

        //Truncate (x,y). This is ok only if values of x and y are really big and rounding is negligible
        long X = (long) x;
        long Y = (long) y;
        IntPoint tmpIntersection = new IntPoint(X,Y);

        //check if the intersection point is inside the segment
        long edgeMinX = (pointA.X <= pointB.X ? pointA.X : pointB.X);
        long edgeMaxX = (pointA.X >= pointB.X ? pointA.X : pointB.X);
        long edgeMinY = (pointA.Y <= pointB.Y ? pointA.Y : pointB.Y);
        long edgeMaxY = (pointA.Y >= pointB.Y ? pointA.Y : pointB.Y);

        intersects = (tmpIntersection.X >= edgeMinX && tmpIntersection.X <= edgeMaxX)
                     &&
                     (tmpIntersection.Y >= edgeMinY && tmpIntersection.Y <= edgeMaxY);

        if (intersects)
            intersection = tmpIntersection;
        else
            intersection = IntPoint.zero;
    }

    /**
    * Same as IntersectionWithLine but this time with another edge.
    * Simply check if the point returned by IntersectionWithLine is contained in this edge.
    **/
    public void IntersectionWithEdge(Edge edge, out bool intersects, out IntPoint intersection)
    {
        IntPoint edgePointA = edge.m_pointA;
        IntPoint edgePointB = edge.m_pointB;

        IntPoint edgeLineIntersection;
        bool bIntersects;
        IntersectionWithLine(edgePointA, edgePointB - edgePointA, out bIntersects, out edgeLineIntersection);

        if (bIntersects)
        {
            long segmentMinX = (edgePointA.X <= edgePointB.X ? edgePointA.X : edgePointB.X);
            long segmentMaxX = (edgePointA.X >= edgePointB.X ? edgePointA.X : edgePointB.X);
            long segmentMinY = (edgePointA.Y <= edgePointB.Y ? edgePointA.Y : edgePointB.Y);
            long segmentMaxY = (edgePointA.Y >= edgePointB.Y ? edgePointA.Y : edgePointB.Y);

            intersects = (edgeLineIntersection.X >= segmentMinX && edgeLineIntersection.X <= segmentMaxX)
                         &&
                         (edgeLineIntersection.Y >= segmentMinY && edgeLineIntersection.Y <= segmentMaxY);

            intersection = edgeLineIntersection;
        }
        else
        {
            intersects = false;
            intersection = IntPoint.zero;
        }       
    }

    /**
     * Tells if this edge contains the point passed as parameter
     * **/
    public bool ContainsPoint(IntPoint point, bool bIncludeEndpoints = true)
    {
        if (point == m_pointA || point == m_pointB)
            return bIncludeEndpoints;

        long det = Geometry.Determinant(m_pointA, m_pointB, point);
        if (det != 0) //points are not aligned, thus point can never be on the segment AB
            return false;

        //points are aligned, just test if points is inside the strip defined by A and B
        IntPoint u = point - m_pointA; //AM vector
        IntPoint v = m_pointB - m_pointA; //AB vector

        long dotProduct = Geometry.DotProduct(u, v); //calculate the dot product AM.AB
        if (dotProduct > 0)
            return dotProduct < v.sqrMagnitude; //AM length should be majored by AB length so dot product AM.AB should be majored by AB squared length
        else //AM and AB are of opposite sign
            return false;
    }
}

/**
 * A triangle with IntPoint vertices
 * **/
public class Triangle
{
    public IntPoint[] m_points { get; set; }

    public Triangle()
    {
        m_points = new IntPoint[3];
    }

    public Triangle(Triangle other)
        : this()
    {
        for (int i = 0; i != 3; i++)
        {
            m_points[i] = other.m_points[i];
        }
    }

    public IntPoint GetCenter()
    {
        return (m_points[0] + m_points[1] + m_points[2]) / 3.0f;
    }

    public bool HasVertex(IntPoint vertex)
    {
        return m_points[0] == vertex || m_points[1] == vertex || m_points[2] == vertex;
    }

    /**
     * Determines if a point is inside this triangle
     * Depending on the vertices order (clockwise or counter-clockwise) the signs of det1, det2 and det3 can be positive or negative
     * so we just check if they are of the same sign
     * Set bStrictlyInside to true if you want to remove cases where point is on the contour
     * **/
    public bool ContainsPoint(IntPoint point, bool bStrictlyInside = false)
    {
        long det1 = Geometry.Determinant(m_points[0], m_points[1], point);
        long det2 = Geometry.Determinant(m_points[1], m_points[2], point);
        long det3 = Geometry.Determinant(m_points[2], m_points[0], point);

        if (bStrictlyInside)
            return (det1 < 0 && det2 < 0 && det3 < 0) || (det1 > 0 && det2 > 0 && det3 > 0);
        else
            return (det1 <= 0 && det2 <= 0 && det3 <= 0) || (det1 >= 0 && det2 >= 0 && det3 >= 0);
    }

    /**
    * Small struct to store an intersection point with additional information such as the indices of edges where this intersection point is located
    **/
    private struct TrianglesIntersection
    {
        public IntPoint m_intersection;
        public int m_triangle1EdgeIndex;
        public int m_triangle2EdgeIndex;
    }

    /**
    * Computes the intersection between 'this' triangle and another triangle
    **/
    public List<IntPoint> IntersectionWithTriangle(Triangle triangle)
    {
        List<TrianglesIntersection> intersections = FindIntersectionsWithTriangle(triangle);
        
        if (intersections.Count == 0) //one triangle is contained into another or both triangles are disjoined
        {
            if (this.ContainsPoint(triangle.GetCenter())) //t2 is inside t1
                return new List<IntPoint>(triangle.m_points);
            else if (triangle.ContainsPoint(this.GetCenter())) //t1 is inside t2
                return new List<IntPoint>(this.m_points);
            else //t1 and t2 are disjoined
                return null;
        }
        else if (intersections.Count == 6) //simply return points of intersection in the order we found them
        {
            List<IntPoint> contour = new List<IntPoint>(4);
            for (int i = 0; i != intersections.Count; i++)
            {
                contour.Add(intersections[i].m_intersection);
            }
            return contour;
        }
        else if (intersections.Count == 2 || intersections.Count == 4) //this covers most intersection cases
        {
            //extract points between two consecutive points
            List<IntPoint> contour = new List<IntPoint>();

            for (int i = 0; i != intersections.Count; i++)
            {
                List<IntPoint> extractedPoints = new List<IntPoint>();
                TrianglesIntersection intersection = intersections[i];
                TrianglesIntersection nextIntersection = intersections[(i == intersections.Count - 1) ? 0 : i + 1];
                ExtractIntersectionContourPoints(extractedPoints, this, triangle, intersection.m_triangle1EdgeIndex, nextIntersection.m_triangle1EdgeIndex);
                if (extractedPoints.Count == 0)
                    ExtractIntersectionContourPoints(extractedPoints, triangle, this, intersection.m_triangle2EdgeIndex, nextIntersection.m_triangle2EdgeIndex);

                contour.AddRange(extractedPoints);
                contour.Add(nextIntersection.m_intersection);
            }

            return contour;
        }
        //there can only be 0, 2, 4 or 6 points of intersections between two triangles. Do not process else statement

        return null;
    }

    /**
    * Find the points of intersection between two triangles. If the intersection is one of the triangles vertices, we do not consider this as a point of intersection.
    * So basically we are searching for edges that strictly intersect and whose intersection is different from the edges endpoints.
    **/
    private List<TrianglesIntersection> FindIntersectionsWithTriangle(Triangle triangle)
    {
        List<TrianglesIntersection> intersections = new List<TrianglesIntersection>();
        for (int i = 0; i != 3; i++)
        {
            Edge edge = new Edge(this.m_points[i], this.m_points[(i == 2) ? 0 : i + 1]);
            bool intersects;
            IntPoint intersection;

            //store the intersection points that are on this edge (max 2)
            TrianglesIntersection[] intersectionsOnEdge = new TrianglesIntersection[2];
            int arrayIndex = 0;
            for (int j = 0; j != 3; j++)
            {
                Edge t2edge = new Edge(triangle.m_points[j], triangle.m_points[(j == 2) ? 0 : j + 1]);
                edge.IntersectionWithEdge(t2edge, out intersects, out intersection);

                if (intersects)
                {
                    //check if the intersection is different from edge endpoints
                    if (intersection == edge.m_pointA ||
                        intersection == edge.m_pointB ||
                        intersection == t2edge.m_pointA ||
                        intersection == t2edge.m_pointB)
                        continue;

                    TrianglesIntersection trianglesIntersection = new TrianglesIntersection();
                    trianglesIntersection.m_intersection = intersection;
                    trianglesIntersection.m_triangle1EdgeIndex = i;
                    trianglesIntersection.m_triangle2EdgeIndex = j;

                    //intersectionPointsOnEdge.Add(intersectionPoint);
                    intersectionsOnEdge[arrayIndex] = trianglesIntersection;
                    arrayIndex++;
                }
            }

            if (arrayIndex == 2) //2 intersection points on this edge
            {
                //reorder them according to the edge direction (from edge pointA to edge pointB)
                float sqrDist1 = (intersectionsOnEdge[0].m_intersection - edge.m_pointA).sqrMagnitude;
                float sqrDist2 = (intersectionsOnEdge[1].m_intersection - edge.m_pointA).sqrMagnitude;

                if (sqrDist1 > sqrDist2)
                {
                    //swap the 2 elements
                    TrianglesIntersection tmp = intersectionsOnEdge[0];
                    intersectionsOnEdge[0] = intersectionsOnEdge[1];
                    intersectionsOnEdge[1] = tmp;
                }

                //add the points to the global list
                intersections.Add(intersectionsOnEdge[0]);
                intersections.Add(intersectionsOnEdge[1]);
            }
            else if (arrayIndex == 1) //1 intersection point on the edge
                intersections.Add(intersectionsOnEdge[0]);
            //else no intersection point, nothing to do
        }

        return intersections;
    }

    /**
    * Extract vertices from t1 whose indices are between edgeStartIndex and edgeStopIndex and that are contained inside t2
    **/
    private void ExtractIntersectionContourPoints(List<IntPoint> extractedPoints, Triangle t1, Triangle t2, int edgeStartIndex, int edgeStopIndex)
    {
        while (edgeStartIndex != edgeStopIndex)
        {
            IntPoint t1Vertex = t1.m_points[(edgeStartIndex == 2) ? 0 : edgeStartIndex + 1];
            if (t2.ContainsPoint(t1Vertex))
                extractedPoints.Add(t1Vertex);

            edgeStartIndex = (edgeStartIndex == 2) ? 0 : edgeStartIndex + 1;
        }
    }

    /**
     * Calculates the area of the triangle
     * **/
    public float GetArea()
    {
        return 0.5f * Mathf.Abs(Geometry.Determinant(m_points[0], m_points[1], m_points[2]));
    }
}