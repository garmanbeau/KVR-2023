using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KVR2023;

public class InputPage : TabletPage
{
    private static int hierarchyIndexNameButton = 0;
    private static int hierarchyIndexKeyButton = 1;
    private static int hierarchyIndexButtonText = 0;
    private TextMeshProUGUI nameButtonText;
    private TextMeshProUGUI keyButtonText;

    private void Awake()
    {
        nameButtonText = this.gameObject.transform.GetChild(hierarchyIndexButtons).GetChild(hierarchyIndexNameButton).GetChild(hierarchyIndexButtonText).gameObject.GetComponent<TextMeshProUGUI>();
        keyButtonText = this.gameObject.transform.GetChild(hierarchyIndexButtons).GetChild(hierarchyIndexKeyButton).GetChild(hierarchyIndexButtonText).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void NamePressed()
    {
        tabletContext.ActivateKeyboard(Inputs.Name);
    }

    public void KeyPressed()
    {
        tabletContext.ActivateKeyboard(Inputs.Key);
    }

    public void SubmitPressed()
    {
        if(nameButtonText.text != "Name" && keyButtonText.text != "Key" && nameButtonText.text.Length > 0 && keyButtonText.text.Length > 0) 
        {
            tabletContext.LoadSpecificPage((int)Pages.Mode_Select);
        }
    }

    public void SetNameButtonText(string newText)
    {
        nameButtonText.text = newText;
    }

    public void SetKeyButtonText(string newText)
    {
        keyButtonText.text = newText;
    }

}
