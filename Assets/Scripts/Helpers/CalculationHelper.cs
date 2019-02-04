using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationHelper
{
    public static float GetDistance(ControllerBase first, ControllerBase second)
    {
        return Vector3.Distance(first.gameObject.transform.position, second.gameObject.transform.position) / 2;
    }

    public static float GetAngle(ControllerBase first, ControllerBase second)
    {
        var target = second.gameObject.transform.position - first.gameObject.transform.position;
        var angle = Vector3.Angle(target, new Vector3(1, 0, 0));
        return target.y < 0 ? -angle : angle;
    }

    public static Vector3 GetDirectionVector(ControllerBase first, ControllerBase second)
    {
        var v1 = first.transform.position;
        var v2 = second.transform.position;

        return v2 - v1;
    }
    
}