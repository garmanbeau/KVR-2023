using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabletButton : MonoBehaviour
{
    private Button button;
    private Color normalColor;
    private void Start()
    {
        button = gameObject.GetComponent<Button>(); //Assign the Button component of the gameObject to button.
        normalColor = button.colors.normalColor; //Store the normalColor set in the inspector.
        CreateColliderFromButtonSize(); //Call private function to create a collider sized appropriately to the button and set it as a trigger.
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
            button.onClick?.Invoke();
        }
    }

    private void CreateColliderFromButtonSize()
    {
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        if (buttonRectTransform != null)
        {
            Vector2 buttonSize = buttonRectTransform.sizeDelta; //Get the size of the button.
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>(); //Add a box collider to the gameObject.
            boxCollider.size = new Vector3(buttonSize.x, buttonSize.y, .00125f); //Set the size of the box collider to the size of the button, but add some girth to the z-axis.
            boxCollider.isTrigger = true; //Make the box collider a trigger so that we can execute logic when the user presses and unpresses the button.
            //Debug.Log("BoxCollider created with size: " + boxCollider.size);
        }
        else
        {
            Debug.LogError("RectTransform component not found on the Button GameObject.");
        }
    }
}
