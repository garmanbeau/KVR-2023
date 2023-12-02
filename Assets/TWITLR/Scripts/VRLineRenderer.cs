using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Autohand;

public class VRLineRenderer : MonoBehaviour
{
    public Transform startPoint;
    public Transform destinationPoint;
    public int curveResolution = 50;
    public float curveHeight = 5f;
    public float lineWidth = 0.1f;
    public Color lineColor = Color.white;
    public float lineGlowIntensity = 1.5f;
    public float pointDelay = 0.05f; // Delay between each point (adjust as desired)

    private LineRenderer lineRenderer;
    public Material lineMaterial;

    private bool isDrawing = false;
    private float pointTimer = 0f;
    private int pointIndex = 0;

   // private Grabbable grabInteractable;
    private Vector3 startPointOffset;
    private Vector3 destinationPointOffset;

    private void Start()
    {
        /*
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.material = new Material(Shader.Find("Unlit/Transparent"));
        lineRenderer.material.color = lineColor;
        lineRenderer.material.EnableKeyword("_EMISSION");
        */
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
     //   grabInteractable = GetComponent<Grabbable>();
        startPointOffset = startPoint.position - transform.position;
        destinationPointOffset = destinationPoint.position - transform.position;

        StopDrawing();
        /*grabInteractable.OnGrabBegin += StartDrawing;
        grabInteractable.OnGrabEnd += StopDrawing;*/
    }

    private void Update()
    {
        if (isDrawing)
        {
            pointTimer += Time.deltaTime;

            if (pointTimer >= pointDelay)
            {
                pointTimer = 0f;
                AddCurvePoint();
            }
        }

        UpdateLinePosition();
    }

    public void StartDrawing()
    {
        lineRenderer.positionCount = 0;
        pointIndex = 0;
        pointTimer = 0f;
        isDrawing = true;
    }

    public void StopDrawing()
    {
        isDrawing = false;
        lineRenderer.positionCount = 0;
    }

    private void AddCurvePoint()
    {
        if (pointIndex <= curveResolution)
        {
            float t = pointIndex / (float)curveResolution;
            Vector3 point = CalculatePointOnQuadraticCurve(startPoint.position, GetMiddlePoint(), destinationPoint.position, t);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(pointIndex, point);
            lineRenderer.material.SetColor("_EmissionColor", lineColor * lineGlowIntensity);
            pointIndex++;
        }
        else
        {
            isDrawing = false;
        }
    }

    private Vector3 GetMiddlePoint()
    {
        Vector3 direction = destinationPoint.position - startPoint.position;
        Vector3 middlePoint = startPoint.position + (0.5f * direction) + (Vector3.up * curveHeight);
        return middlePoint;
    }

    private Vector3 CalculatePointOnQuadraticCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }

    private void UpdateLinePosition()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, destinationPoint.position);
    }
}
