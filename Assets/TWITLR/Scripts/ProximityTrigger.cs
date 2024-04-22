using UnityEngine;
using UnityEngine.Events;

public class ProximityTrigger : MonoBehaviour
{
 //   public LayerMask robotLayer;
 //   public LayerMask playerLayer;

  //  public RobotAI targetRobot;
  //  private bool robotIsMoving = false;
 //   public UnityEvent onRobotEnter;
    public int clipPlayIndex = 0;

    /*
    void Start()
    {
        if (targetRobot == null)
        {
            GameObject robotObject = GameObject.FindGameObjectWithTag("Robot");
            if (robotObject != null)
            {
                targetRobot = robotObject.GetComponent<RobotAI>();
            }
            else
            {
                Debug.LogWarning("ProximityTrigger: Could not find a Robot.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.gameObject == targetRobot)
        {
            Debug.Log("Robot Entered Trigger");
            onRobotEnter?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
          //  StopRobotMovement();
        }
    }

    void StartRobotMovement()
    {
        if (targetRobot != null && !robotIsMoving)
        {
            targetRobot.MoveToPosition(transform.position);
            robotIsMoving = true;
        }
    }

    void StopRobotMovement()
    {
        if (targetRobot != null && robotIsMoving)
        {
            targetRobot.StopMoving();
            robotIsMoving = false;
        }
    }
    */
}
