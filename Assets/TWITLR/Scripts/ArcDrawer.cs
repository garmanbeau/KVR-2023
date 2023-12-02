using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcDrawer : MonoBehaviour
{

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField][Range(0, 10)] private float height = 1f;

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3 startTangent;
    private Vector3 endTangent;

    public float heightCoef = 20;

    [SerializeField][Range(10, 100)] private int segments = 50;
    private void Update()
    {
        UpdatePositions();
        DrawCurve();
    }

    private void UpdatePositions()
    {
        height = Vector3.Distance(startTransform.position, endTransform.position) / heightCoef;

       
        startPos = startTransform.position;
        endPos = endTransform.position;

      

        startTangent = (startPos + Vector3.up * height) - startPos;

        endTangent = endPos - (endPos + Vector3.up * -height) -(Vector3.up * height);
    }

    private void DrawCurve()
    {
     
        lineRenderer.positionCount = segments + 1;

        for (int i = 0; i < segments + 1; i++)
        {
            float t = i / (float)segments;
            lineRenderer.SetPosition(i, CalculateBezierPoint(t));
        }
    }

    private Vector3 CalculateBezierPoint(float t)
    {
     

        Vector3 point =
            (1 - t) * (1 - t) * startPos +
            2 * (1 - t) * t * (startPos + startTangent) +
            t * t * (endPos + endTangent);

        return point;
    }

    public void Show()
    {
        lineRenderer.enabled = true;
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
    }
}
