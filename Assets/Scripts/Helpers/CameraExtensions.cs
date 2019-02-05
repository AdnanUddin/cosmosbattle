using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class CameraExtension
{
    public static Bounds GetCameraBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            
         return bounds;
    }
    
}