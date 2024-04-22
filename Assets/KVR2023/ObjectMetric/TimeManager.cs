using UnityEngine;

public class TimeManager: MonoBehaviour
{
    public float timeSinceTestSelection;
    public float levelTimer = 0.0f;
    private bool updateTimer = false;

    public void StartTime() 
    {
        updateTimer = true;
        timeSinceTestSelection = Time.time;
    }
    public void Update()
    {
    if (updateTimer)
        levelTimer += Time.deltaTime * 1;
    //timerTex = levelTimer.ToString("f2"); //Time to texture with 2 decimal
    }
    public void LevelEnded()
    {
    updateTimer = false;
    PlayerPrefs.SetFloat("Time In Second", levelTimer); //Keep Complete Time for compare and save
    }
}


