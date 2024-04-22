using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class InstructionListTextHandler : MonoBehaviour
{
    public ProgramContainer programContainer;
    public TMP_Text instructionsText;

    void Start()
    {
        

        // Initial update
        UpdateInstructionListText();
    }

   public void UpdateInstructionListText()
    {
        if (instructionsText == null) return;

        instructionsText.text = "";
        foreach (var instruction in programContainer.instructions)
        {
            string instructionText = GetFormattedInstructionText(instruction);
            instructionsText.text += instructionText + "\n"; // Add new line for each instruction
        }
    }

    private string GetFormattedInstructionText(ProgramContainer.Instruction instruction)
    {
        string colorHex = GetColorHexForInstructionType(instruction.type);
        return $"<color={colorHex}>{instruction.type.ToString()}: {instruction.value.ToString()}</color>";
    }

    private string GetColorHexForInstructionType(ProgramContainer.InstructionType type)
    {
        switch (type)
        {
            case ProgramContainer.InstructionType.MOVE:
                return "#FFFFFF"; // White
            case ProgramContainer.InstructionType.ROTATE:
                return "#00FFFF"; // Cyan
            case ProgramContainer.InstructionType.ACTION:
                return "#FFFF00"; // Yellow
            default:
                return "#FFFFFF"; // Default to white
        }
    }


}
