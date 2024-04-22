using KVR2023;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is the implementation select the farthest planet in the KVR2023 Sprint6Scene. 
/// When the user grabs Neptune in the scene, an event is triggered. 
/// </summary>
public class SelectFarthestPlanetTask : TaskBase
{
    [SerializeField] private VisualAffordance VisualAffordance;
    public UnityEvent selectFarthestPlanetComplete;
    private bool hasGrabbedWhileTaskActive;
    private int incorrectGrabCount;

    public void Start()
    {
        incorrectGrabCount = 0;
        directions = "Grab the farthest planet in our solar system.";
    }

    public override void StartTask()
    {
        this.TabletContext.UpdateCurrentPageText(1, directions); //To Do: Get rid of magic numbers.
    }

    public override void StopTask()//nothing happens when stop task is called.
    {
        stopAffordances?.Invoke();
        return;
    }

    public void PlanetGrabbed(int num)
    {
        if (IsActive)
        {
            Planets planets = (Planets)num;
            switch (planets)
            {
                case Planets.Mercury:
                    incorrectGrabCount++;
                    break;
                case Planets.Venus:
                    incorrectGrabCount++;
                    break;
                case Planets.Earth:
                    incorrectGrabCount++;
                    break;
                case Planets.Mars:
                    incorrectGrabCount++;
                    break;
                case Planets.Jupiter:
                    incorrectGrabCount++;
                    break;
                case Planets.Saturn:
                    incorrectGrabCount++;
                    break;
                case Planets.Uranus:
                    incorrectGrabCount++;
                    break;
                case Planets.Neptune:
                    taskManager.TaskCompletionUpdate();
                    selectFarthestPlanetComplete?.Invoke();
                    break;
            }
        }
    }
}
