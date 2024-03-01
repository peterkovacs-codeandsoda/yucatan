using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaBoundController : Singleton<GameAreaBoundController>
{
    private Bounds bounds;
    void Start()
    {
        bounds = new Bounds();
        Vector2[] points = GetComponent<PolygonCollider2D>().points;
        foreach (var point in points)
        {
            bounds.Encapsulate(point);
        }
    }

    public Vector2 KeepInBound(Vector2 initialPosition)
    {
        return bounds.ClosestPoint(initialPosition);
    }

    public Vector2 RandomPositionInBound()
    {
        return new(Random.Range(-15, 15), Random.Range(-15, 15));
    }


}
