using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SubsPage : TabletPage
{
    public List<GameObject> buttons;
    [SerializeField] private GameObject beginButton;
    [SerializeField] private TaskManager taskManager;
    private HorizontalLayoutGroup layoutGroup;
    private static readonly float desiredSpacing = .04f;
    private static readonly int hierarchyIndexButtonGroup = 1;

    private void Awake() //Invoked by the engine when an instance of this class is instantiated (or activated if instantiated as inactive).
    {
        layoutGroup = this.gameObject.transform.GetChild(hierarchyIndexButtonGroup).gameObject.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.spacing = 0f;
    }

    public void BeginPressed() //Called via OnClick? event of a TabletButton.
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
        }
        beginButton.SetActive(false);
        layoutGroup.spacing = desiredSpacing;
        taskManager.CurrentTask.CompleteEmptySub();
    }

    public void ResetPage() //Called via OnClick? event of a TabletButton.
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        beginButton.SetActive(true);
        layoutGroup.spacing = 0f;
    }
}
