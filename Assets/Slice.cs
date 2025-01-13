using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public float minSliceDistance = 0.05f;
    private List<Vector2> points;
    private LineRenderer lineRenderer;
    public int maxPoints = 40;
    private Canvas canvas;  // Ajoutez un canvas pour les conversions d'écran

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.8f;
        lineRenderer.endWidth = 3f;
        lineRenderer.widthMultiplier = 10.0f;
        points = new List<Vector2>();
        canvas = FindObjectOfType<Canvas>();  // Trouver le canvas pour la conversion
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // PC version
        {
            UpdateSlice(Input.mousePosition);
        }
        else if (Input.touchCount > 0) // Mobile version
        {
            UpdateSlice(Input.GetTouch(0).position);
        }
        else
        {
            ResetSlice();
        }
    }

    void UpdateSlice(Vector2 inputPosition)
    {
        // Convertir la position de l'écran en coordonnées locales de l'UI (Canvas)
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), inputPosition, canvas.worldCamera, out localPoint);

        // Si c'est un nouveau point ou si l'écart est assez grand, ajoutez-le à la liste
        if (points.Count == 0 || Vector2.Distance(points[points.Count - 1], localPoint) > minSliceDistance)
        {
            points.Add(localPoint);

            if (points.Count > maxPoints)
            {
                points.RemoveAt(0);
            }

            lineRenderer.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                // Utiliser les coordonnées locales pour définir la position
                lineRenderer.SetPosition(i, points[i]);
            }
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
