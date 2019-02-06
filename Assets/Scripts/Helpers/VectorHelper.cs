using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelper
{

    public static Vector3 Rotate(this Vector3 vector, float angle)
    {
        var length = vector.magnitude;
        vector.x = length * Mathf.Cos(angle);
        vector.y = length * Mathf.Sin(angle);

        return vector;
    }

    public static Vector3 Translate(this Vector3 vector, Vector3 translateVector)
    {   
        vector.x += translateVector.x;
        vector.y += translateVector.y;
        return vector;
    }

    public static Vector3[] CreateTriangle(float scale = 1.0f)
    {
        var cameraBounds  = Camera.main.GetCameraBounds();
        var trianglePoints = new Vector3[3];

        var maxLength = (2 * cameraBounds.max.x) / scale;
        trianglePoints[0] = Vector3.zero;
        trianglePoints[1] = new Vector3(UnityEngine.Random.Range(maxLength / 2, maxLength), 0f, 0f);

        var x = UnityEngine.Random.Range(0.0f, trianglePoints[1].x);
        var y = UnityEngine.Random.Range(maxLength / 2, maxLength);
        trianglePoints[2] = new Vector3(x, y, 0);
        
        return trianglePoints;
    }

    public static Vector3 GetTriangleCentre(Vector3[] coords)
    {
        var Ox = (coords[0].x + coords[1].x + coords[2].x) / 3.0f;
        var Oy = (coords[0].y + coords[1].y + coords[2].y) / 3.0f;

        return new Vector3(Ox, Oy, 0.0f);
    }

    public static Vector3 GetRandomPoint()
    {
        var bounds = Camera.main.GetCameraBounds();

        var point = new Vector3
        {
            x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            z = 0.0f
        };

        return point;        
    }
}