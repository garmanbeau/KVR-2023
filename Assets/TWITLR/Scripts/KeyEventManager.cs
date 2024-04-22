using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyEventManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyboardEvent
    {
        public KeyCode keycode;
        public UnityEvent keyEvent;
        public UnityEvent keyReleaseEvent;
        public bool triggerOnPress = true; // If false, event will trigger on key down
    }

    public List<KeyboardEvent> keyboardEvents = new List<KeyboardEvent>();

    void Update()
    {
        foreach (var keyboardEvent in keyboardEvents)
        {
            if ((keyboardEvent.triggerOnPress && Input.GetKeyDown(keyboardEvent.keycode)) ||
                (!keyboardEvent.triggerOnPress && Input.GetKey(keyboardEvent.keycode)))
            {
                keyboardEvent.keyEvent.Invoke();
            }
            if (Input.GetKeyUp(keyboardEvent.keycode))
            {
                keyboardEvent.keyReleaseEvent.Invoke();
            }
        }
    }
}
