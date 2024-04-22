// 2023-12-26 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChange : MonoBehaviour
{
    public int sceneIndexToLoad;
    public float delay = 3f;
    private bool locked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HandPlayer") && !locked)
        {
            StartCoroutine(LoadSceneWithDelay());
            locked = true; // locks it so the change scene is not called multiple times
        }
    }


    public void SetLevel (int level)
    {
        sceneIndexToLoad = level;
    }
    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndexToLoad);
        locked = false; //unlocks it when the scene is actually loaded
    }
}
