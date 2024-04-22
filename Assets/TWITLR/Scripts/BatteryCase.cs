using UnityEngine;
using System.Collections.Generic;

public class BatteryCase : MonoBehaviour
{
    public List<BatteryReceiver> batteryReceivers;
    public ProgramContainer programContainer;

    public void DestroyAllVisibleObjects()
    {
        foreach (var receiver in batteryReceivers)
        {
            receiver.DestroyVisibleObject();
        }
    }

    public void ProcessValidReceivers()
    {
        foreach (var receiver in batteryReceivers)
        {
            if (receiver.visibleObject != null)
            {
                InstructionBlock instructionBlock = receiver.visibleObject.GetComponent<InstructionBlock>();
                if (instructionBlock != null)
                {
                    Debug.Log("Adding Instruction: " + instructionBlock.instruction.type.ToString());
                    programContainer.AddInstruction( instructionBlock.instruction.type, instructionBlock.instruction.value);
                }
            }
        }
    }
}
