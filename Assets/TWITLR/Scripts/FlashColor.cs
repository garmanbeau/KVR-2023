using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
   
    public MeshRenderer targetRenderer;
    public Color offColor = Color.black;
    public Color onColor = Color.white;
    public float onTime = 0.5f;
    public float offTime = 1.5f;

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        Material material = targetRenderer.material;

        // Lerp to onColor over onTime duration
        float elapsedTime = 0.0f;
        while (elapsedTime < onTime)
        {
            float t = elapsedTime / onTime;
            material.SetColor("_Color", Color.Lerp(offColor, onColor, t));
            material.SetColor("_EmissionColor", Color.Lerp(offColor, onColor, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set to onColor just to make sure it reaches the target value
        material.SetColor("_Color", onColor);
        material.SetColor("_EmissionColor", onColor);

        // Lerp back to offColor over offTime duration
        elapsedTime = 0.0f;
        while (elapsedTime < offTime)
        {
            float t = elapsedTime / offTime;
            material.SetColor("_Color", Color.Lerp(onColor, offColor, t));
            material.SetColor("_EmissionColor", Color.Lerp(onColor, offColor, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set to offColor to ensure it reaches the target value
        material.SetColor("_Color", offColor);
        material.SetColor("_EmissionColor", offColor);
    }
}
