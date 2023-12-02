using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartHPQuiz : MonoBehaviour
{
    public AudioSource play;
    public AudioClip StartAudio;
    public Button OkButton;
    public TextMeshProUGUI text;
    public GameObject QuestionPanel;
    public GameObject AnswerPanel;
    public GameObject ActionPanel;
    public HPQuizController controller;
    public string StartText;
    private float length;
    private string currentText;
    private float delay = 0.01f;

    // Start is called before the first frame update
    void Awake()
    {
        length = StartAudio.length;
        StartCoroutine(InitialText());
        Invoke("PlayAudio", 0.5f);
        Invoke("button", length);
    }

    private void PlayAudio()
    {
        play.PlayOneShot(StartAudio);
    }


    private void button()
    {
        OkButton.interactable = true;
    }

    public void Begin()
    {
        QuestionPanel.SetActive(true);
        AnswerPanel.SetActive(true);
        ActionPanel.SetActive(true);
        controller.FirstQuestion();
        this.gameObject.SetActive(false);
    }

    IEnumerator InitialText()
    {
        for (int i = 0; i < StartText.Length; i++)
        {
            currentText = StartText.Substring(0, i);
            text.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        string lastChar = StartText.Substring(StartText.Length - 1);
        Debug.Log("LastChar is" + lastChar);
        currentText += lastChar;
        text.text = currentText;
    }

}
    
