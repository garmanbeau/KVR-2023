using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RobotAI : MonoBehaviour
{
    public Transform playerTransform;
    public float followDistance = 5.0f;
    public Transform[] guidePoints;
    public Animator robotAnimator;
    public AudioClipPlayer audioPlayer;
    

    private NavMeshAgent agent;
    private enum State { Companion, Guide, Idle }
    private State currentState = State.Companion;
    private float idleTimer = 0.0f;
    private const float idleTimeout = 15.0f;
    private int currentGuideTarget = 0;

    public UnityEvent onTargetReached; // Public UnityEvent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Companion:
                FollowPlayer();
                break;
            case State.Guide:
                LeadPlayer();
                break;
            case State.Idle:
                IdleBehavior();
                break;
        }
    }


    void FollowPlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Keep movement on the level plane

        Vector3 targetPosition = playerTransform.position; // Target the player directly

        if (directionToPlayer.magnitude > followDistance)
        {
            MoveToPosition(targetPosition);
        }
        else
        {
            StopMoving(); // Stop once close enough
            ResetIdleTimer();
            onTargetReached.Invoke(); // Trigger the event
        }
    }

    void ResetIdleTimer()
    {
        idleTimer = 0.0f;
        robotAnimator.SetBool("Move", false); // Update animation 
    }
    void LeadPlayer()
    {
        if (HasReachedTarget())
        {
            robotAnimator.SetBool("Move", false); // Stop walking animation if needed
            return; // We're already at the current target
        }

        agent.SetDestination(guidePoints[currentGuideTarget].position);
        agent.isStopped = false;
        robotAnimator.SetBool("Move", true);
    }

    bool HasReachedTarget()
    {
        // Adjust the threshold as needed 
        return Vector3.Distance(transform.position, guidePoints[currentGuideTarget].position) < 1.0f;
    }

    public void MoveToNextPoint()
    {
        currentGuideTarget = (currentGuideTarget + 1) % guidePoints.Length;
        LeadPlayer();  // Start movement towards the new target
    }

    public void MoveToPreviousPoint()
    {
        currentGuideTarget--;
        if (currentGuideTarget < 0)
        {
            currentGuideTarget = guidePoints.Length - 1; // Wrap around
        }
        LeadPlayer();
    }

    public void MoveToPoint(int index)
    {
        if (index >= 0 && index < guidePoints.Length)
        {
            currentGuideTarget = index;
            LeadPlayer();
        }
        else
        {
            Debug.LogWarning("Invalid index for MoveToPoint");
        }
    }

    void IdleBehavior()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleTimeout)
        {
            MoveToPlayer();
            idleTimer = 0.0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            Debug.Log("Trigger Entered");
            ProximityTrigger proxTrigger = other.GetComponent<ProximityTrigger>();
            if (proxTrigger != null)
            {
                audioPlayer.PlayOnce(proxTrigger.clipPlayIndex);
                TriggerTalkAnimation();
            }
            
        }
    }

    public void TriggerTalkAnimation()
    {
        StartCoroutine(TalkAnimationCoroutine());
    }

    private IEnumerator TalkAnimationCoroutine()
    {
        // Set the "Talk" parameter to true
        robotAnimator.SetBool("Talk", true);

        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Set the "Talk" parameter to false
        robotAnimator.SetBool("Talk", false);
    }

    void MoveToPlayer()
    {
        Vector3 targetPosition = playerTransform.position + (playerTransform.forward * followDistance);
        MoveToPosition(targetPosition);
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        agent.isStopped = false;
        robotAnimator.SetBool("Move", true);
    }

    public void StopMoving()
    {
        agent.isStopped = true;
        robotAnimator.SetBool("Move", false);
    }
    public void StartTalking()
    {
        robotAnimator.SetBool("Move", true);
    }

    public void StopTalking()
    {
        robotAnimator.SetBool("Action", false);
        agent.isStopped = false; // Allow the agent to move again
    }

    // ... (StartTalking, StopTalking, ResetIdleTimer - from earlier examples) 
}
