using UnityEngine;
using UnityEngine.Events;
using KVR2023;

namespace KVR2023
{
    public enum Planets
    {
        Mercury = 0,
        Venus = 1,
        Earth = 2,
        Mars = 3,
        Jupiter = 4,
        Saturn = 5,
        Uranus = 6,
        Neptune = 7,
    }
}


/// <summary>
/// This class is the implementation select the largest planet in the KVR2023 Sprint6Scene. 
/// When the user grabs Jupiter in the scene, an event is triggered. 
/// </summary>
public class SelectLargestPlanetTask : TaskBase
{
    public UnityEvent selectLargestPlanetComplete;
    private bool hasGrabbedWhileTaskActive;
    private int incorrectGrabCount;

    public void Start()
    {
        incorrectGrabCount = 0;
        directions = "Grab the largest planet in our solar system.";
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
                    taskManager.TaskCompletionUpdate();
                    selectLargestPlanetComplete?.Invoke();
                    break;
                case Planets.Saturn:
                    incorrectGrabCount++;
                    break;
                case Planets.Uranus:
                    incorrectGrabCount++;
                    break;
                case Planets.Neptune:
                    incorrectGrabCount++;
                    break;
            }
        }
    }

    public override void StartTask()
    {
        this.TabletContext.UpdateCurrentPageText(1, directions); //To Do: Get rid of magic numbers.
    }

    public override void StopTask()//the stop affordance event should contain link to the affordance.StopAffordance
    {
        stopAffordances?.Invoke();
        return;
    }
}
