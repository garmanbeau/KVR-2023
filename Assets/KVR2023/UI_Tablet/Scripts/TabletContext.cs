using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KVR2023;

public class TabletContext : MonoBehaviour
{
    private List<TabletPage> pages = new List<TabletPage>();
    [field: SerializeField] public NetworkRequestManager networkRequestManager { get; set; }
    [SerializeField] private float pageLoadDelay;
    [SerializeField] private TabletPage initialPage;
    private TabletPage currentPage;
    private int pageIndex;
    public string playerName { get; set; }
    public string playerKey { get; set; }
    private Inputs inputEnum;

    private void Awake() //Invoked by the engine when this object is instanted (or activated if instantiated as inactive).
    {
        this.PopulatePages();
        this.SetInitialValues();
    }

    private void PopulatePages()
    {
        foreach(Transform childTransform in this.gameObject.transform)
        {
            pages.Add(childTransform.gameObject.GetComponent<TabletPage>());
        }
    }

    private void SetInitialValues()
    {
        int indexCount = 0; //local variable used to determine and assign a value to the private field pageIndex.
        foreach (TabletPage page in pages) //This loop sets non-initial objects to inactive while determining the index position of the initialPage.
        {
            page.SetInitialValues(this); //We give every page a reference to the MenuPageManager
            if (page == initialPage) //Check each page in the list to find the initialPage.
            {
                pageIndex = indexCount; //When the initialPage is found, set the pageIndex to the index of the initialPage in the list of objects.
            }
            else //If a page in the list is not the initialPage.
            {
                page.gameObject.SetActive(false); //Deactivate the page GameObject.
            }
            indexCount++; //Increment index count to ensure the correct index value is assigned.
        }
        if (initialPage == null && pages[0] != null) //Special Case: User did not assign an initialPage, but they did add GameObjects to the list of objects.
        {
            currentPage = pages[0]; //Assign initialPage to the first page in the list.
            pageIndex = 0; //Set pageIndex equal to the index of the first page in the list.
        }
        else if (initialPage != null) //Else if initialPage has been set by the user.
        {
            currentPage = initialPage; //Assign the currentObject to initialPage.
        }
        else
        {
            Debug.LogError("The tablet doesn't have any pages.");
        }
        currentPage.gameObject.SetActive(true); //Activate the currentObject.
        //Debug.Log("Starting pageIndex = " + pageIndex);
    }

    private IEnumerator LoadDelayCoroutine() //This coroutine is used to create a delay between setting the currentObject inactive and the next currentObject active.
    {//This function is invoked by other functions in this class after they deactivate the currentObject.
        yield return new WaitForSeconds(pageLoadDelay); //Wait for a period of time in seconds that is equal to objectLoadDelay.
        currentPage.gameObject.SetActive(true); //Activate the new currentObject now that the delay has completed.
        //Debug.Log("LoadDelayCoroutine completing with pageIndex = " + pageIndex);
    }

    public void LoadSpecificPage(int index) //Function that accepts an integer value and uses it to load the page with the corresponding index value in objects.
    { //Called via UnityEvent when user presses a button on the tablet.
        //Debug.Log("LoadSpecificobj called when objIndex = " + pageIndex);
        if (pages[index] != null) //Ensure that the passed index value corresponds to a page in the list.
        {
            currentPage.gameObject.SetActive(false); //Deactivate the current page.
            pageIndex = index; //Set objIndex to the passed index value.
            currentPage = pages[pageIndex]; //Assign currentObject to the page with the passed index in the list of objs.
            //Debug.Log("LoadSpecificObject calling LoadDelayCoroutine with pageIndex = " + pageIndex);
            StartCoroutine(LoadDelayCoroutine()); //Trigger objDelayCoroutine(), which activates the new currentObject after a short delay.
        }
    }

    public void UpdateCurrentPageText(int textId, string newText)
    {
        this.currentPage.UpdateText(textId, newText);
    }

    public void ActivateKeyboard(Inputs newInput)
    {
        inputEnum = newInput;
        this.LoadSpecificPage((int)Pages.Keyboard);
        TabletKeyboard keyboard = pages[(int)Pages.Keyboard].gameObject.GetComponent<TabletKeyboard>();
        keyboard.ActivateKeyboard(inputEnum);
    }

    public void DeactivateKeyboard(string newText)
    {
        switch(inputEnum)
        {
            case Inputs.Name:
                {
                    playerName = newText;
                    this.LoadSpecificPage((int)Pages.Input);
                    InputPage inputPage = pages[(int)Pages.Input].gameObject.GetComponent<InputPage>();
                    inputPage.SetNameButtonText(playerName);
                    break;
                }
            case Inputs.Key:
                {
                    playerKey = newText;
                    this.LoadSpecificPage((int)Pages.Input);
                    InputPage inputPage = pages[(int)Pages.Input].gameObject.GetComponent<InputPage>();
                    inputPage.SetKeyButtonText(playerKey);
                    break;
                }
        }
    }
}
