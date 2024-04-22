using UnityEngine;

public class MaterialTrigger : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material offMaterial;
    public Material onMaterial;

    private void Start()
    {
        // Set the material to offMaterial when the game starts
        meshRenderer.material = offMaterial;
    }

    public void TriggerMaterial()
    {
        meshRenderer.material = onMaterial;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.material = onMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.material = offMaterial;
        }
    }
}
