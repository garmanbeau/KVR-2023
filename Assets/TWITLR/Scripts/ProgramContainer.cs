// 2024-01-21 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramContainer : MonoBehaviour
{
    
    public enum InstructionType
    {
        MOVE,
        ROTATE,
        ACTION
    }

    [Serializable]
    public struct Instruction
    {
        public InstructionType type;
        public int value;
        public bool completed;  // Added completed field

        public Instruction(InstructionType type, int value)
        {
            this.type = type;
            this.value = value;
            this.completed = false;  // Initialising completed as false
        }
    }

    public UnityEvent OnInstructionsChanged;

  

    public static ProgramContainer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of ProgramContainer found!");
        }

      //  LoadSampleInstructions();
    }

    [SerializeField]
    public List<Instruction> instructions = new List<Instruction>();
    private int currentIndex = 0;  // Keep track of the current instruction index

    public void AddInstruction(InstructionType type, int value)
    {
        instructions.Add(new Instruction(type, value));
        OnInstructionsChanged?.Invoke(); // Trigger the event
    }

    public void RemoveInstruction(int index)
    {
        if (index >= 0 && index < instructions.Count)
        {
            instructions.RemoveAt(index);
            OnInstructionsChanged?.Invoke(); // Trigger the event
            if (index <= currentIndex && currentIndex > 0)  // Adjust the currentIndex if needed
            {
                currentIndex--;
            }
        }

    }

    public void LoadSampleInstructions()
    {
        // Clear current instructions
        instructions.Clear();

        // Add sample instructions
        AddInstruction(InstructionType.MOVE, 1);
        AddInstruction(InstructionType.ROTATE, 90);
        AddInstruction(InstructionType.MOVE, 1);
        AddInstruction(InstructionType.ROTATE, -90);
        AddInstruction(InstructionType.MOVE, 1);
        AddInstruction(InstructionType.ACTION, 1);
        AddInstruction(InstructionType.MOVE, 1);
        AddInstruction(InstructionType.ROTATE, 90);
        AddInstruction(InstructionType.MOVE, 1);
        AddInstruction(InstructionType.ROTATE, -90);
        AddInstruction(InstructionType.MOVE, 1);
        AddInstruction(InstructionType.ACTION, 1);
    }


    // Method to move to the next instruction in the list
    public Instruction? MoveToNextInstruction()
    {
        currentIndex++;
        if (currentIndex < instructions.Count)
        {
            return GetCurrentInstruction();  // Retrieve the next instruction
        }
        return null;
    }

    // Method to move to a specific instruction in the list
    public Instruction? MoveToInstruction(int index)
    {
        if (index >= 0 && index < instructions.Count)
        {
            currentIndex = index;
            return GetCurrentInstruction();  // Retrieve the specified instruction
        }
        return null;
    }

    public void ClearInstructionsList()
    {
       
        instructions.Clear();
        currentIndex = 0;
        OnInstructionsChanged?.Invoke(); // Trigger the event
    }
    // Private method to get the current instruction
    private Instruction? GetCurrentInstruction()
    {
        if (currentIndex >= 0 && currentIndex < instructions.Count)
        {
            return instructions[currentIndex];
        }
        return null;
    }
}
