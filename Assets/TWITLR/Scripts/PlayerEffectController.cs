// 2023-12-26 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using UnityEngine.Events;

public class PlayerEffectController : MonoBehaviour
{
    public UnityEvent triggerEffect;
    public UnityEvent delayEvent;
    public Animator animator;
    public float delay = 3f;
    public string triggerName;

    public MeshRenderer renderer; // Added MeshRenderer variable

    void Start()
    {
        // Disable the MeshRenderer on startup
      //  renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
        animator.enabled = false;
    }

    public void PlayAnimationOnce()
    {
        // Enable the MeshRenderer when the animation plays
        renderer.enabled = true;

        animator.enabled = true;
    }

    public void TriggerEffectMethod()
    {
        triggerEffect?.Invoke();

        if (delayEvent != null)
        {
            Invoke("DelayEventMethod", delay);
        }
    }

    private void DelayEventMethod()
    {
        delayEvent?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == triggerName)
        {
            TriggerEffectMethod();
        }
    }
}
