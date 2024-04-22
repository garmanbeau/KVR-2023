using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Each button on the tablet must include an instance of this class as a component.  
//This class will add a box collider component to the gameObject it is attached to
//and automatically size it according to the size of the button component.  


public class TabletButton : MonoBehaviour
{
    private Button button;
    private Color normalColor;
    private char? keyOutput;
    private TextMeshProUGUI textComponent;
    private TabletKeyboard tabletKeyboard;
    private static readonly float zAxisGirth = .00125f;
    private static readonly int hierarchyIndexText = 0;

    private void Start()
    {
        button = gameObject.GetComponent<Button>(); //Assign the Button component of the gameObject to button.
        button.onClick?.AddListener(OnRayTrigger);//neccessary if user want to use UI Cursor so select letters on keyboard

        normalColor = button.colors.normalColor; //Store the normalColor set in the inspector.
        textComponent = transform.GetChild(hierarchyIndexText).gameObject.GetComponent<TextMeshProUGUI>(); //Assign reference to text component on child gameObject.
        CreateColliderFromButtonSize(); //Call private function to create a collider sized appropriately to the button and set it as a trigger.
    }

    // Custom method to handle ray intersection
    //Method calls string concatentation of tabletKeyboard when pressed
    //This method is necessary to ensure  users can use UI cursor to select buttons. 
    private void OnRayTrigger()
    {
        Debug.Log("Trigger enter with Ray.");

        if (keyOutput != null)
        {
            Debug.Log("Concatenating keyboard string");
            tabletKeyboard.ConcatenateString(keyOutput);
        }

    }
    private void OnTriggerEnter(Collider other) //Called when one of the player's index fingers starts colliding with the box collider.
    {
        if(other.tag == "PlayerFinger") //To do: Consider finding a different way to ensure that only the player's index finger triggers the button gameObject's collider.
        {
            Debug.Log("Trigger enter with PlayerFinger.");
            ColorBlock colors = button.colors; //Store a reference to the ColorBlock of the button.
            colors.normalColor = colors.pressedColor; //Change the normal color in the ColorBlock to the pressed color.
            button.colors = colors; //Set the button's colors.
        }
    }

    private void OnTriggerExit(Collider other) //Called when one of the player's index fingers stops colliding with the box collider.
    {
        if(other.tag == "PlayerFinger") //To do: Consider finding a different way to ensure that only the player's index finger triggers the button gameObject's collider.
        {
            Debug.Log("Trigger exit with PlayerFinger.");
            ColorBlock colors = button.colors; //Store the ColorBlock of the button.
            colors.normalColor = normalColor; //Set the normal color in the color block back to the original normal color.
            button.colors = colors; //Set the button's colors to the colors in the ColorBlock.
            button.onClick?.RemoveListener(OnRayTrigger); //remove listener before button events are invoked, otherwise will add two characters to string
            button.onClick?.Invoke();
            if(keyOutput != null)
            {
                tabletKeyboard.ConcatenateString(keyOutput); //Add the keyOutput char to the tabletKeyboard's stringBuilder.
            }
            button.onClick?.AddListener(OnRayTrigger); //add back listener so user has option to use UI cursor.
        }
    }

    private void CreateColliderFromButtonSize()
    {
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        if (buttonRectTransform != null)
        {
            Vector2 buttonSize = buttonRectTransform.sizeDelta; //Get the size of the button.
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>(); //Add a box collider to the gameObject.
            boxCollider.size = new Vector3(buttonSize.x, buttonSize.y, zAxisGirth); //Set the size of the box collider to the size of the button, but add some girth to the z-axis.
            boxCollider.isTrigger = true; //Make the box collider a trigger so that we can execute logic when the user presses and unpresses the button.
            //Debug.Log("BoxCollider created with size: " + boxCollider.size);
        }
        else
        {
            Debug.LogError("RectTransform component not found on the Button GameObject.");
        }
    }

    public void SetKeyboardReference(TabletKeyboard keyboard) //Invoked by TabletKeyboard page as part of the char? array mapping algorithm
    {
        tabletKeyboard = keyboard;
    }

    public void SetKeyOutput(char? newOutput) //Invoked by TabletKeyboard page as part of the char? array mapping algorithm
    {
        keyOutput = newOutput;
    }

    public void ToggleCapsLock()
    {
        if(keyOutput != null && keyOutput != ' ') 
        {
            char tempChar = (char)keyOutput;
            if(char.IsUpper(tempChar))
            {
                tempChar = char.ToLower(tempChar);
                textComponent.text = tempChar.ToString();
                keyOutput = tempChar;
            }
            else
            {
                tempChar = char.ToUpper(tempChar);
                textComponent.text = tempChar.ToString();
                keyOutput = tempChar;
            }
        }
    }
}
