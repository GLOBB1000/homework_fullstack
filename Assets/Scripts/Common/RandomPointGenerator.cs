using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomPointGenerator
{
    public static Transform RandomPoint(Transform[] points)
    {
        int index = Random.Range(0, points.Length);
        return points[index];
    }
}
