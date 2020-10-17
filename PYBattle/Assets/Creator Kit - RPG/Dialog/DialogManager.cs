using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog Dialog)
    {
        UnityEngine.Debug.Log("Start convo" + Dialog.name);

        sentences.Clear();

        foreach(string sentence in Dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        UnityEngine.Debug.Log(sentence);

    }

    void EndDialog()
    {
        UnityEngine.Debug.Log("End convo");
    }

}
