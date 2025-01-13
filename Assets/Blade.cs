using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float minSliceDistance = 0.05f;
    private List<Vector2> points;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Vector2>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Appui de la souris
        {
            UpdateSlice(Input.mousePosition);
        }
        else if (Input.touchCount > 0) // Appui du doigt sur mobile
        {
            UpdateSlice(Input.GetTouch(0).position);
        }
        else
        {
            ResetSlice();
        }
    }

    void UpdateSlice(Vector3 inputPosition)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        if (points.Count == 0 || Vector2.Distance(points[points.Count - 1], worldPosition) > minSliceDistance)
        {
            points.Add(worldPosition);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, worldPosition);
        }
    }

    void ResetSlice()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
    }

    public List<Vector2> GetSlicePoints()
    {
        return new List<Vector2>(points);
    }
}
