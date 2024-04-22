using KVR2023;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace KVR2023
{
    public enum Inputs
    {
        Name = 0,
        Key = 1,
    }
}

public class TabletKeyboard : TabletPage
{
    private List<TabletButton> buttons = new List<TabletButton>();
    private List<TabletButton> spcButtons = new List<TabletButton>();
    private static readonly int rowCount = 4;
    private StringBuilder stringBuilder;
    private Inputs inputEnum;
    private static readonly int textHeader = 0;
    private static readonly int textInput = 1;
    private bool specialButtonsAssigned = false;
    private GameObject defaultButtons;
    private GameObject specialButtons;
    private static readonly int hierarchyIndexSpecialButtons = 2;

    private void Awake() //Invoked by the engine when an instance of this class is instantiated (or activated if instantiated as inactive) alongside its corresponding gameObject
    {
        this.defaultButtons = transform.GetChild(hierarchyIndexButtons).gameObject; //Assigning GameObject reference based on hierarchy.
        this.specialButtons = transform.GetChild(hierarchyIndexSpecialButtons).gameObject; //Assigning GameObject reference based on hierarchy.
        //Note: Do not change the order of the tablet keyboard game objects in the hierarchy.
        //HierarchyIndexButtons is defined in the abstract base class Tablet Page.  It is the same for every page.
        //HierarchyIndexSpecialButtons is defined in this class, as it is unique to the TabletKeyboard page.
        //New button sets can be added, but they must be placed after the existing button sets with the same parent in the hierarchy.

        //Start of mapping algorithm used to provide default keyboard buttons with a reference to this TabletKeyboard class and the correct char? key value.
        char?[] keyLayout = 
        { //This nullable character array is pre-assigned values that correspond to the positions of the keyboard button game objects in the hierarchy.
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o','p',
            'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',
            'z', 'x', 'c', 'v', 'b', 'n', 'm', null,
            null, null, ' ', null,
        }; //Keyboard buttons without a key input character must be represented with a null entry in the array.
        for (int i = 0; i < rowCount; i++) //Loop through once for each row.
        {
            GameObject row = defaultButtons.transform.GetChild(i).gameObject; //Start loop by getting the next row (which are all direct children of the vertical layout group gameObject)
            for(int j = 0; j < row.transform.childCount; j++) //loop through once for each button in the current row
            {
                buttons.Add(row.transform.GetChild(j).gameObject.GetComponent<TabletButton>()); //Add the buttons to the list such that their index values match the 
            }                                                                                   //index values of the corresponding nullable characters in the array.
        }
        //Debug.Log("ArraySize = " + keyLayout.Length);
        //Debug.Log("ListSize = " + buttons.Count);
        if (buttons.Count == keyLayout.Length) //Ensure that the number of keyboard buttons matches 
        {                                      //the number of characters before attempting to map them
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetKeyOutput(keyLayout[i]); //Key output can be set to null which prevents the button from trying to concatenate string when pressed.
                buttons[i].SetKeyboardReference(this); //All keyboard buttons require a reference to enact their will on the keyboard. 
            }
        }
        else
        {
            Debug.LogError("The number of buttons doesn't match the number of nullable characters.");
        }
    }

    public void ToggleToSpecial() //Invoked via OnClick? event of TabletButton on the default buttons set of the TabletKeyboard.
    {
        this.defaultButtons.SetActive(false);
        this.specialButtons.SetActive(true);
        if(!specialButtonsAssigned) //The key values for the default set are mapped in the Awake() function, but additional button sets require their own mapping.
        {                           //This mapping should only occur once per button set, hence the specialButtonsAssigned bool.
            char?[] specialKeyLayout =
            { //Keyboard buttons without a key input character must be represented with a null entry in the array.
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                '!', '@', '#', '$', '%', '^', '&', '*', '(', ')',
                '-', '_', '=', '+', '[', ']', '{', '}', null,
                '?', '.', null, null, null,
            };
            for (int i = 0; i < rowCount; i++) //Loop through once for each row.
            {
                GameObject row = specialButtons.transform.GetChild(i).gameObject; //Start loop by getting the next row (which are all direct children of the vertical layout group gameObject)
                for (int j = 0; j < row.transform.childCount; j++) //loop through once for each button in the current row
                {
                    spcButtons.Add(row.transform.GetChild(j).gameObject.GetComponent<TabletButton>()); //Add the buttons to the list such that their index values match the 
                }                                                                                   //index values of the corresponding nullable characters in the array.
            }
            if (spcButtons.Count == specialKeyLayout.Length) //Ensure that the number of keyboard buttons matches 
            {                                             //the number of characters before attempting to map them
                for (int i = 0; i < spcButtons.Count; i++)
                {
                    spcButtons[i].SetKeyOutput(specialKeyLayout[i]);
                    spcButtons[i].SetKeyboardReference(this); //All keyboard buttons require a reference to enact their will on the keyboard. 
                }
            }
            else
            {
                Debug.LogError("The number of buttons doesn't match the number of nullable characters.");
            }
            specialButtonsAssigned = true;
        }
    }

    public void ToggleToDefault() //Invoked via OnClick event of TabletButton on the special buttons set of the TabletKeyboard.
    {
        this.specialButtons.SetActive(false);
        this.defaultButtons.SetActive(true);
    }

    public void ActivateKeyboard(Inputs newInputEnum) //Invoked by TabletContext on behalf of the InputPage.
    {
        inputEnum = newInputEnum;
        this.UpdateText(textHeader, "Enter " + inputEnum.ToString() + ":");
        stringBuilder = new StringBuilder();
        string newText = stringBuilder.ToString();
        this.UpdateText(textInput, newText);
    }

    public void ConcatenateString(char? letter) //Invoked by TabletButton when the button is pressed and char? keyOutput != null
    {
        stringBuilder.Append(letter);
        string newText = stringBuilder.ToString();
        this.UpdateText(textInput, newText);
    }

    public void DecatenateString() //Invoked via the Delete TabletKeyboard TabletButton's OnClick event.
    {
        if(stringBuilder.Length > 0)
        {
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            string newText = stringBuilder.ToString();
            this.UpdateText(textInput, newText);
        }
    }

    public void ToggleCapsLock()
    {
        foreach(var button in buttons) 
        {
            button.ToggleCapsLock();
        }
    }

    public void SubmitData()
    {
        string submission = stringBuilder.ToString();
        if (submission.Length > 0)
        {
            tabletContext.DeactivateKeyboard(submission);
        }
    }
}
