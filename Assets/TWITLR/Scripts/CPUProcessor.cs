using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CPUProcessor : MonoBehaviour
{

    public List<Bit> bits;
    public TMP_Text outputText;

    [SerializeField]
    string[] asciiSymbols = new string[256];
    public TMP_FontAsset tmpFontAsset;


    public string wordToLookFor; // The word to look for
    public UnityEvent onWordFound; // Event that's triggered when the whole word is found
    public UnityEvent onWordComplete; //Event that is invoked when a word is completed.

    private string asciiString = "";
    private int currentCharIndex = 0; // Index to keep track of which character in wordToLookFor we're trying to match

    public TMP_Text wordToLookForText;
    public TMP_Text outPutWord;
    public TMP_Text hintText;

    public int wordsToSpell = 3;
    int wordsSpelled = 0;
    public UnityEvent onAllWordsSpelled;

    string[] commonWords = {
    "about", "Apple", "asked", "After", "could", "Every", "first", "Green", "house",
    "large", "Learn", "never", "Other", "place", "point", "Paris", "Right", "small",
    "Sound", "spell", "Their", "three", "Water", "which", "World", "write", "Being",
    "below", "Above", "eight", "Tokyo", "heart", "Night", "People", "CPU", "Paris",
    "Direct", "listen", "Method", "paper", "Person", "Father", "Sydney", "Rome", "Berlin",
    "Create", "animal", "figure", "Minute", "Lima", "system", "Nature", "though", "Letter",
    "Delhi", "follow", "parent", "Mexico", "center", "Matter", "health", "Others", "Reading",
    "Science", "Oslo", "against", "Vienna", "return", "Cairo", "London", "Lisbon", "Madrid"
};
    public bool wordComplete = true;

    private void Start()
    {
        for (int i = 0; i < asciiSymbols.Length; i++)
        {
            if (tmpFontAsset.characterLookupTable.ContainsKey((uint)i))
            {
                asciiSymbols[i] = ((char)i).ToString();
            }
        }

     //  Reset();
     
    }

    private void Reset()
    {
        if (wordsSpelled >= wordsToSpell)
        {
            wordToLookForText.text = "Congrats!";
            hintText.text = "Level Complete";
            outPutWord.text = "Level Complete!";
            onAllWordsSpelled?.Invoke();

        }
        else
        {
            int index = Random.Range(0, commonWords.Length);
            wordToLookFor = commonWords[index];
            wordToLookForText.text = wordToLookFor;
            hintText.text = GetAsciiValuesOfString();
            asciiString = "";
            outPutWord.text = "";
            currentCharIndex = 0;
            wordsSpelled++;

        }
        foreach (var bit in bits)
        {
            bit.bitText.text = "0";
            Calculate();
        }

    }

    public void Calculate()
    {
        int value = 0;
        foreach (var bit in bits)
        {
            if (bit.bitText.text == "1")
            {
                value += bit.fieldValue;
            }

        }

        outputText.text = value.ToString();

        if (!wordComplete)
        {
            AddToAsciiString(value);
        }
    }

    public string GetAsciiValuesOfString()
    {
        string asciiValues = "";
        for (int i = 0; i < wordToLookFor.Length; i++)
        {
            if (i > 0) // Add a comma before adding the next value, but not for the first value
            {
                asciiValues += ",";
            }
            asciiValues += ((int)wordToLookFor[i]).ToString();
        }
        return asciiValues;
    }

    public void NextWord()
    {

        if (!wordComplete) { return; }
        wordComplete = false;

        onWordComplete?.Invoke(); //invoke the onWordComplete event when word is complete and NextWord is called

        Reset();
    }
    public void AddToAsciiString(int asciiValue)
    {
        if (asciiValue >= 0 && asciiValue < 128 && currentCharIndex < wordToLookFor.Length)
        {
            if (asciiSymbols[asciiValue] == wordToLookFor[currentCharIndex].ToString())
            {
                asciiString += asciiSymbols[asciiValue];
                outPutWord.text = asciiString;
                currentCharIndex++;

                if (currentCharIndex == wordToLookFor.Length)
                {
                   // Invoke("Reset", 2);
                   wordComplete = true;
                    onWordFound.Invoke();
                    Debug.Log("Word found: " + asciiString);
                }
            }
        }
        else
        {
            Debug.LogWarning($"The provided value {asciiValue} is not a valid ASCII value, or the word has already been found.");
        }
    }

}
