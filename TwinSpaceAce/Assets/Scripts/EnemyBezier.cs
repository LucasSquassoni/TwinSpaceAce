using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform player;
    public int numPoints;
    public int offsetMax;

    private Vector3[] positions;
    private Vector3 point0, point1, point2;

    public Vector3[] Positions { get => positions; set => positions = value; }

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[numPoints];

        DrawQuadraticCurve();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawQuadraticCurve()
    {
        point0 = transform.position;
        point2 = player.position;

        Vector3 dir = (point2 - point0).normalized;
        Vector3 perDir = Vector3.Cross(dir, -Vector3.up * Random.Range(1, offsetMax));
        Vector3 midPoint = (point0 + point2) / 2;
        Vector3 offsetPoint = midPoint + perDir;

        point1 = offsetPoint;
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(point0, point1, point2, t);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        //B(t) = (u)^2P0 + 2(u)tP1 + tsquareP2

        float u = 1 - t;
        float uSquare = u * u;
        float tsquare = t * t;

        Vector3 point = uSquare * p0;
        point += 2 * u * t * p1;
        point += tsquare * p2;

        return point;
    }
}
