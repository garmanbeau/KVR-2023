using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletPageLoader : MonoBehaviour //An instance of this class is instantiated when the GameObject it is attached to is instantiated.
{
    public List<GameObject> pages; //Holds references to all GameObjects that are children of the UI_Tablet GameObject.
    [SerializeField] private float pageLoadDelay; //The time in seconds that it takes for the next page to activate after the currentPage is deactivated.
    [SerializeField] private GameObject initialPage; //Set in the inspector.  If it isn't set, it will be assigned to the GameObject with index 0 in pages.
    private TextUpdateManager textUpdateManager; //Set in the inspector.  Ensures that TextUpdates trigger after a new page is loaded.
    private GameObject currentPage; //Assigned to the active page.  This reference is used to activate and deactivate pages.
    private int pageIndex; //Used to reassign the currentPage.

    void Start() //Called via Unity Magic when the gameObject this script/class is attached to is instantiated by the engine. 
    {
        int indexCount = 0; //local variable used to determine and assign a value to the private field pageIndex.
        foreach (GameObject page in pages) //This loop sets non-initial pages to inactive while determining the index position of the initial page.
        {
            if (page == initialPage) //Check each page in the list to find the initialPage.
            {
                pageIndex = indexCount; //When the initialPage is found, set the pageIndex to the index of the initialPage in the list of pages.
            }
            else //If a page in the list is not the initialPage.
            {
                page.SetActive(false); //Deactivate the page GameObject.
            }
            indexCount++; //Increment index count to ensure the correct index value is assigned.
        }
        if (initialPage == null && pages[0] != null) //Special Case: User did not assign an initialPage, but they did add GameObjects to the list of pages.
        {
            currentPage = pages[0]; //Assign initialPage to the first page in the list.
            pageIndex = 0; //Set pageIndex equal to the index of the first page in the list.
        }
        else if (initialPage != null) //Else if initial page has been set by the user.
        {
            currentPage = initialPage; //Assign the currentPage to initialPage.
        }
        currentPage.SetActive(true); //Activate the currentPage.
        //Debug.Log("Starting pageIndex = " + pageIndex);
    }

    public void LoadSpecificPage(int index) //Function that accepts an integer value and uses it to load the page corresponding to the index value in pages.
    { //Called via UnityEvent when user presses a button on the tablet.
        //Debug.Log("LoadSpecificPage called when pageIndex = " + pageIndex);
        if (pages[index] != null) //Ensure that the passed index value corresponds to a page in the list.
        {
            currentPage.SetActive(false); //Deactivate the current page.
            pageIndex = index; //Set pageIndex to the passed index value.
            currentPage = pages[pageIndex]; //Assign currentPage to the page with the passed index in the list of pages.
            //Debug.Log("LoadSpecificPage calling PageDelayCoroutine with pageIndex = " + pageIndex);
            StartCoroutine(PageDelayCoroutine()); //Trigger PageDelayCoroutine(), which activates the new currentPage after a short delay.
        }
    }


    public void LoadNextPage() //Function that loads the next page in the ordered list of pages.
    { //Called via UnityEvent when user presses a button on the tablet.
        //Debug.Log("LoadNextPage() called with pageIndex = " + pageIndex);
        if (pageIndex < pages.Count - 1) //Ensure that there is a next page before trying to load it.
        {
            currentPage.SetActive(false); //Deactivate the currentPage.
            pageIndex++; //Increment pageIndex to match the index of the next page in the list of pages.
            currentPage = pages[pageIndex]; //Assign currentPage to the next page in the list.
            //Debug.Log("LoadNextPage() calling PageDelayCoroutine with pageIndex = " + pageIndex);
            StartCoroutine(PageDelayCoroutine()); //Trigger PageDelayCoroutine(), which activates the new currentPage after a short delay.
        }
    }

    public void LoadPreviousPage() //Function that loads the previous page in the ordered list of pages. It can be invoked by a UnityEvent.
    { //Called via UnityEvent when user presses a button on the tablet.
        //Debug.Log("LoadPreviousPage() called with pageIndex = " + pageIndex);
        if (pageIndex > 0) //Ensure that there is a previous page before trying to load it.
        {
            //Debug.Log("Page Index before loading previous page = " + pageIndex);
            currentPage.SetActive(false); //Deactivate the current page.
            pageIndex--; //Decrement pageIndex to match the index of the previous page in the list of pages.
            currentPage = pages[pageIndex]; //Assign currentPage to the previous page in the list of pages.
            //Debug.Log("LoadPreviousPage() calling PageDelayCoroutine with pageIndex = " + pageIndex);
            StartCoroutine(PageDelayCoroutine()); //Trigger PageDelayCoroutine(), which activates the new currentPage after a short delay.
        }
    }

    private IEnumerator PageDelayCoroutine() //This coroutine is used to create a delay between setting the currentPage inactive and the next currentPage active.
    {//This function is invoked by other functions in this class after they deactivate the currentPage.
        yield return new WaitForSeconds(pageLoadDelay); //Wait for a period of time in seconds that is equal to pageLoadDelay.
        currentPage.SetActive(true); //Activate the new currentPage now that the delay has completed.
        //Debug.Log("PageDelayCoroutine completing with pageIndex = " + pageIndex);
    }
}
