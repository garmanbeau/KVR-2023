using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class counter : MonoBehaviour
{
    // Start is called before the first frame update
    public int nBase = 10;

    public string[] symbols;
    public float delay = 1.0f;
    float timer = 0;
    int index = 0;

    public bool head = false;
    public counter next;
    public TMP_Text text;

    public manager man;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        // text.text = symbols[index];
        text.text = "-";
    }

    // Update is called once per frame
    void Update()
    {
        if (!head) return;
        if (man.running)
        {
            timer += Time.deltaTime;
            if (timer >= man.delay)
            {
                timer = 0.0f;

                Increment();
            }
        }
    }

    public void Increment()
    {
        index++;
        if (index >= nBase)
        {
            index = 0;
            if (next != null)
            {
                next.Increment();
            }
        }
        text.text = symbols[index];

    }
}
