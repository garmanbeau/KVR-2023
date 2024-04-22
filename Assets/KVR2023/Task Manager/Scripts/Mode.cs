using UnityEngine;

public class Mode : MonoBehaviour
{
    public static Mode Instance { get; private set; }

    [field: SerializeField]
    public string ModeName { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}