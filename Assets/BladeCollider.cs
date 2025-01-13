using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class BladeCollider : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    private Slice blade;
    List<Vector2> emptyList = new List<Vector2>();
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        blade = GetComponent<Slice>();
    }

    void Update()
    {
        List<Vector2> points = blade.GetSlicePoints();

        if (points.Count > 1)
        {
            edgeCollider.SetPoints(points);
            edgeCollider.enabled = true;
        }
        else
        {
            edgeCollider.enabled = false;
        }
    }
}
