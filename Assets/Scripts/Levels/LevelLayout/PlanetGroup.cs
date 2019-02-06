using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetGroup
{

    private const float leftRightPadding = 0.01f;
    private const float topBottomPadding = 1f;

    public PlanetGroup(Vector3 startPosition, float scale = 1.0f)
    {
        this.coordinates = VectorHelper.CreateTriangle(scale: scale);
        this.GroupCenter = VectorHelper.GetTriangleCentre(this.coordinates);
        this.MoveTo(startPosition);
    }

    public Vector3[] coordinates {get; set;}

    public Vector3 GroupCenter {get; set; }

    // rotate from origin
    public void Rotate(float angle)
    {
        this.coordinates[0] = this.coordinates[0].Rotate(angle);
        this.coordinates[1] = this.coordinates[1].Rotate(angle);
        this.coordinates[2] = this.coordinates[2].Rotate(angle);
        this.GroupCenter = VectorHelper.GetTriangleCentre(this.coordinates);
    }

    public void MoveToOrigin()
    {
        this.MoveTo(-this.GroupCenter);
    }

    public void MoveTo(Vector3 newPosition)
    {
        var cameraBounds = Camera.main.GetCameraBounds();

        var translateVector = newPosition - this.GroupCenter;
        this.coordinates[0] = this.coordinates[0].Translate(translateVector);
        this.coordinates[1] = this.coordinates[1].Translate(translateVector);
        this.coordinates[2] = this.coordinates[2].Translate(translateVector);
        this.GroupCenter = VectorHelper.GetTriangleCentre(this.coordinates);

        // check if coordinates are within viewable space
        var planetGroupBounds = this.GetBounds();
        var left = planetGroupBounds.min.x;
        var right = planetGroupBounds.max.x;

        var top = planetGroupBounds.max.y;
        var bottom = planetGroupBounds.min.y;

        translateVector = Vector3.zero;

        if (left < cameraBounds.min.x)
        {
            translateVector.x += (cameraBounds.min.x - left) + leftRightPadding;
        }

        if (right > cameraBounds.max.x)
        {
            translateVector.x -= (cameraBounds.min.x - right) - leftRightPadding;
        }

        if (bottom < cameraBounds.min.y)
        {
            translateVector.y += (cameraBounds.min.y - bottom) + topBottomPadding;
        }

        if (top > cameraBounds.max.y)
        {
            translateVector.y -= (cameraBounds.min.x - top) - topBottomPadding;
        }

        this.coordinates[0] = this.coordinates[0].Translate(translateVector);
        this.coordinates[1] = this.coordinates[1].Translate(translateVector);
        this.coordinates[2] = this.coordinates[2].Translate(translateVector);

        this.GroupCenter = VectorHelper.GetTriangleCentre(this.coordinates);
    }

    public Bounds GetBounds()
    {
        var top = this.coordinates.Max(c => c.y);
        var bot = this.coordinates.Min(c => c.y);
        var left = this.coordinates.Min(c => c.x);
        var right = this.coordinates.Max(c => c.x);
        
        return new Bounds()
        {
            center = this.GroupCenter,
            max = new Vector3(right, top),
            min = new Vector3(left, bot)
        };
    }

    public bool IsInside(PlanetGroup planetGroup)
    {
        var planetBounds = planetGroup.GetBounds();
        var bounds = this.GetBounds();
        
         return planetBounds.center.x < bounds.max.x && planetBounds.center.x > bounds.min.x
            && planetBounds.center.y > bounds.min.y && planetBounds.center.y < bounds.max.y;
    }
}