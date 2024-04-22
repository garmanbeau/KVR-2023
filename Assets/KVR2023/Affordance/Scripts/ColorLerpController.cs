using UnityEngine;
using System.Collections;

public class ColorLerpController : MonoBehaviour, IAffordance
{
    public MeshRenderer targetRenderer;
    public float onTime = 1.0f;
    public float offTime = 1.0f;
    public Color onColor = Color.white;
    public Color offColor = Color.black;

    private Material material;
    private bool isTriggered = false;

    private void Start()
    {
        if (targetRenderer == null)
        {
            Debug.LogError("MeshRenderer reference is not set!");
            return;
        }

        material = targetRenderer.material;
        if (material == null)
        {
            Debug.LogError("Material not found on the MeshRenderer!");
            return;
        }
    }

    public void Trigger()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(ColorLerpRoutine());
        }
    }
    public void StopAffordance()
    {
        StopAllCoroutines();
        material.SetColor("_Color", Color.white);
        material.SetColor("_EmissionColor", Color.black);
    }

    private IEnumerator ColorLerpRoutine()
    {
        float elapsedTime = 0f;

        // Lerp to onColor
        while (elapsedTime < onTime)
        {
            float t = elapsedTime / onTime;
            material.SetColor("_Color", Color.Lerp(offColor, onColor, t));
            material.SetColor("_EmissionColor", Color.Lerp(offColor, onColor, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        material.SetColor("_Color", onColor);
        material.SetColor("_EmissionColor", onColor);

        elapsedTime = 0f;

        // Lerp back to offColor
        while (elapsedTime < offTime)
        {
            float t = elapsedTime / offTime;
            material.SetColor("_Color", Color.Lerp(onColor, offColor, t));
            material.SetColor("_EmissionColor", Color.Lerp(onColor, offColor, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        material.SetColor("_Color", offColor);
        material.SetColor("_EmissionColor", offColor);

        isTriggered = false;
    }

}