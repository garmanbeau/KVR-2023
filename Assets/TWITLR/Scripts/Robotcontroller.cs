// 2024-01-21 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using static ProgramContainer;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class RobotController : MonoBehaviour
{
    public ProgramContainer programContainer;
    public float moveSpeed;
    public float rotationSpeed;
    public float activateDelay;
    public int actionIndex = 0;
    public bool canDoAction = false;
    public ActionHandler actionHandler;

    public UnityEvent OnActionInstruction;

    public Animator animator;

    void Start()
    {
        programContainer = ProgramContainer.instance;

      // actionHandler = GameObject.Find("ActrionHandler").GetComponent<ActionHandler>();


        //StartInstruction(firstInstruction);
    }

    public void BeginRun()
    {
        var firstInstruction = programContainer.instructions[0];
        StartInstruction(firstInstruction);
    }



    // This method is called whenever another collider enters the trigger collider attached to this object.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the gameObject's name of the collider that entered the trigger is "PlayerCatcher"
        if (other.gameObject.name == "PlayerCatcher")
        {
            // Stop all coroutines running on this script
            StopAllCoroutines();
        }
    }

    // 2024-01-21 AI-Tag 
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    void StartInstruction(ProgramContainer.Instruction instruction)
    {
        // Check if the instruction is null before attempting to start a coroutine
       // if (instruction.HasValue)
        {
            switch (instruction.type)
            {
                case InstructionType.MOVE:
                    StartCoroutine(MoveCoroutine(instruction));
                    break;
                case InstructionType.ROTATE:
                    StartCoroutine(RotateCoroutine(instruction));
                    break;
                case InstructionType.ACTION:
                    StartCoroutine(ActivateCoroutine(instruction));
                    break;
                default:
                    break;
            }
        }
       /* else
        {
            Debug.Log("No more instructions to process");
            programContainer.ClearInstructionsList();
        }*/
    }


    IEnumerator MoveCoroutine(ProgramContainer.Instruction instruction)
    {

        animator.SetBool("Move", true);
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + transform.forward * instruction.value;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            yield return null;
        }
        transform.position = targetPosition;
        animator.SetBool("Move", false);

        // Mark the instruction as completed
        instruction.completed = true;

        // Move to the next instruction
        ProgramContainer.Instruction? nextInstruction = programContainer.MoveToNextInstruction();
        if (nextInstruction != null)
        {
            StartInstruction(nextInstruction.Value);
        }
        else
        {
            Debug.Log("No more instructions to process");
        }
    }



    // 2024-01-21 AI-Tag 
    // This was created with assistance from Muse, a Unity Artificial Intelligence product

    IEnumerator RotateCoroutine(ProgramContainer.Instruction instruction)
    {
        // Starting rotation
       
        Quaternion startRotation = transform.rotation;

        // Calculate the target rotation based on the current rotation
        Quaternion targetRotation = startRotation * Quaternion.Euler(new Vector3(0, instruction.value, 0));

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        transform.rotation = targetRotation;
        // Mark the instruction as completed
        instruction.completed = true;

        // Move to the next instruction
        ProgramContainer.Instruction? nextInstruction = programContainer.MoveToNextInstruction();
        if (nextInstruction != null)
        {
            StartInstruction((ProgramContainer.Instruction)nextInstruction);
        }
        else
        {
            Debug.Log("No more instructions to process");
        }
    }


    IEnumerator ActivateCoroutine(ProgramContainer.Instruction instruction)
    {
        Debug.Log("ACTION");
        animator.SetBool("Action", true);        // Wait for the delay
        yield return new WaitForSeconds(activateDelay);

        // Perform ACTIVATE action here
        // ...

        if (canDoAction)
        {
           
            actionHandler.MarkActionAsCompleted(actionIndex); // Mark the first action as completed

            OnActionInstruction?.Invoke(); //currently used to call taskManger.TextCompleteUpdate() to complete a task
        }

        // Mark instruction as completed
        instruction.completed = true;
        animator.SetBool("Action", false);

        // Move to the next instruction
        StartInstruction((ProgramContainer.Instruction)programContainer.MoveToNextInstruction());
    }
}
