using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using KVR2023;

namespace KVR2023
{
    public enum Pages
    {
        Start = 0,
        Input = 1,
        Keyboard = 2,
        Mode_Select = 3,
        Sandbox = 4,
        Instruction = 5,
        Test = 6,
        Subs = 7,
        Complete = 8,
    }
}

public class TabletPage : MonoBehaviour
{
    [SerializeField] protected Pages pageEnum;
    protected TabletContext tabletContext;
    protected List<TextMeshProUGUI> textComponents = new List<TextMeshProUGUI>();
    protected static int hierarchyIndexTextComponents = 0;
    protected static int hierarchyIndexButtons = 1;

    public void SetInitialValues(TabletContext newTabletContext)
    {
        this.tabletContext = newTabletContext;
        foreach (Transform childTransform in this.transform.GetChild(hierarchyIndexTextComponents))
        {
            textComponents.Add(childTransform.gameObject.GetComponent<TextMeshProUGUI>());
        }
    }

    public void UpdateText(int textId, string newText)
    {
        textComponents[textId].text = newText;
    }
}