// 2023-12-21 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Needed for UnityEvent

public class ProgressMarker : MonoBehaviour
{
    public List<MeshRenderer> meshRenderers;
    public Material onMaterial;
    public Material offMaterial;
    public UnityEvent OnCompleted; // The UnityEvent that will be invoked when all renderers have been activated

    private int activatedCount = 0;

    void Start()
    {
        // Assign the 'offMaterial' to each of the MeshRenderers at start.
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material = offMaterial;
        }
    }

    public void Increment()
    {
        // Check if there are still MeshRenderers left in the list to activate.
        if (activatedCount < meshRenderers.Count)
        {
            // Change the material of the first MeshRenderer in the list to the 'onMaterial' 
            meshRenderers[activatedCount].material = onMaterial;

            // Increment the 'activatedCount' variable.
            activatedCount++;

            // If all MeshRenderers have been activated, invoke the 'OnCompleted' event.
            if (activatedCount == meshRenderers.Count)
            {
                OnCompleted?.Invoke();
            }
        }
    }
}
