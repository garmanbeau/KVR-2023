using UnityEngine;
using UnityEngine.Events;

public class ActionTrigger : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material offMaterial;
    public Material onMaterial;
    public int actionIndex = 0;
    public UnityEvent onAction; 
    private void Start()
    {
        // Set the material to offMaterial when the game starts
        meshRenderer.material = offMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change the material
            meshRenderer.material = onMaterial;

            // Set CanDoAction to true if RobotController is found
            RobotController robotController = other.GetComponent<RobotController>();
            if (robotController != null)
            {
                robotController.canDoAction = true;
                robotController.actionIndex = actionIndex;

                onAction?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change the material back to offMaterial
            meshRenderer.material = offMaterial;

            // Set CanDoAction to false if RobotController is found
            RobotController robotController = other.GetComponent<RobotController>();
            if (robotController != null)
            {
                robotController.canDoAction = false;
            }
        }
    }
}
