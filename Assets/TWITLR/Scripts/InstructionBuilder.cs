using UnityEngine;
using TMPro; // Make sure to include the TMPro namespace

public class InstructionBuilder : MonoBehaviour
{
    public ProgramContainer programContainer;
    public TMP_Text typeText; // Reference to the text object for type
    public TMP_Text valueText; // Reference to the text object for value

    private ProgramContainer.Instruction currentInstruction;

    void Start()
    {
        if (programContainer == null)
        {
            programContainer = GetComponent<ProgramContainer>();
        }
      //  CreateNewInstruction();
    }

    public void CreateNewInstruction()
    {
        currentInstruction = new ProgramContainer.Instruction(); // Initialize a new Instruction
        ClearTextFields(); // Clear text fields when a new instruction is created
    }

    private void ClearTextFields()
    {
        if (typeText != null) typeText.text = "";
        if (valueText != null) valueText.text = "";
    }

    public void SetInstructionType(int type)
    {
        

        switch (type)
        {
            case 0: currentInstruction.type = ProgramContainer.InstructionType.ROTATE; break;
            case 1: currentInstruction.type = ProgramContainer.InstructionType.MOVE; break;
            case 2: currentInstruction.type = ProgramContainer.InstructionType.ACTION; break;

        }

        {
           
            UpdateTypeText(currentInstruction.type.ToString()); // Update type text
        }
    }

    private void UpdateTypeText(string type)
    {
        if (typeText != null) typeText.text = "Type: " + type;
    }

    public void SetInstructionValue(int value)
    {
      
        {
            currentInstruction.value = value; // Set the instruction value
            UpdateValueText(value.ToString()); // Update value text
        }
    }

    private void UpdateValueText(string value)
    {
        if (valueText != null) valueText.text = "Value: " + value;
    }

    public void AddInstructionToList()
    {
        if (programContainer != null )
        {
            programContainer.instructions.Add(currentInstruction); // Add the instruction to the list in ProgramContainer
        }
    }
}
