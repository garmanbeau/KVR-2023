using System.Collections;
using UnityEngine;
using TMPro;

public class TextualAffordance : MonoBehaviour, IAffordance
{
    [SerializeField] private GameObject affordanceTextObject;
    private TextMeshPro affordanceTextComponent;
    public int timeAffordanceShown;

    private void Start()
    {
        if (affordanceTextObject == null)
        {
            Debug.LogError("AffordanceText reference is not set!");
            return;
        }
        //affordanceTextObject.SetActive(false);
        affordanceTextComponent = affordanceTextObject.GetComponent<TextMeshPro>();

    }
    public void Trigger()
    {
        StartCoroutine(ShowTextRoutine());
    }
    public void StopAffordance()
    {
        StopAllCoroutines();
        affordanceTextObject.SetActive(false);
    }

   IEnumerator ShowTextRoutine()
    {
        if (timeAffordanceShown > 0)
        {
            affordanceTextObject.SetActive(true);

            yield return new WaitForSeconds(timeAffordanceShown);

            affordanceTextObject.SetActive(false);
        }
    }

    public void SetAffordanceText(string newText)
    {
        affordanceTextComponent.text = newText;
    }

    public void SetAffordanceActive(bool isActive)
    {
        affordanceTextObject.SetActive(isActive);
    }

}
