using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppRestart : MonoBehaviour
{
  public void RestartApp()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeScene (int id)
    {
        SceneManager.LoadScene(id);
    }
    public void ResetTheScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    }
}
