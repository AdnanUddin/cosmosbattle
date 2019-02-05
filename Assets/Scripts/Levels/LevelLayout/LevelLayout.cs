using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLayout
{
    // creates 3 points representing a random triangle
    public static Vector3[] CreateTriangle()
    {
        var cameraBounds  = Camera.main.GetCameraBounds();
        var trianglePoints = new Vector3[3];

        var maxLength = cameraBounds.max.x / 5.0f;
        trianglePoints[0] = Vector3.zero;
        trianglePoints[1] = new Vector3(UnityEngine.Random.Range(1.0f, maxLength), 0f, 0f);

        var x = UnityEngine.Random.Range(0.0f, trianglePoints[1].x);
        var y = UnityEngine.Random.Range(2.0f, maxLength);
        trianglePoints[2] = new Vector3(x, y, 0);
        
        return trianglePoints;
    }
}