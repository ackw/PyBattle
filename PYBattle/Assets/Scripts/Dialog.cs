using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [Serializable]
    public struct Questions
    {
        public string question;
        public string op1;
        public string op2;
        public string op3;
        public string op4;
        public string correct_ans;
    }

    Questions[] allResults;

    public Text inQuestion;
    public Button in1;
    public Button in2;
    public Button in3;
    public Button in4;

    public int questionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        

        StartCoroutine(GetData());

        in1.onClick.AddListener(() =>
        {
            if(string.Equals("1", allResults[questionIndex].correct_ans))
            {
                print("Correct Ans");
                questionIndex += 1;
                PlayerPrefs.SetInt("qIndex", questionIndex);
                SceneManager.UnloadSceneAsync("Dialog");
            }
            else
            {
                print("Wrong Ans");
            }
       
        });

        in2.onClick.AddListener(() =>
        {
            if (string.Equals("2", allResults[questionIndex].correct_ans))
            {
                print("Correct Ans");
                questionIndex += 1;
                PlayerPrefs.SetInt("qIndex", questionIndex);
                SceneManager.UnloadSceneAsync("Dialog");
            }
            else
            {
                print("Wrong Ans");
            }

           
        });

        in3.onClick.AddListener(() =>
        {
            if (string.Equals("3", allResults[questionIndex].correct_ans))
            {
                print("Correct Ans");
                questionIndex += 1;
                PlayerPrefs.SetInt("qIndex", questionIndex);
                SceneManager.UnloadSceneAsync("Dialog");
            }
            else
            {
                print("Wrong Ans");
            }

            
        });

        in4.onClick.AddListener(() =>
        {
            if (string.Equals("4", allResults[questionIndex].correct_ans))
            {
                print("Correct Ans");
                questionIndex += 1;
                PlayerPrefs.SetInt("qIndex", questionIndex);
                SceneManager.UnloadSceneAsync("Dialog");
            }
            else
            {
                print("Wrong Ans");
            }

            
        });
    }

    void DrawUI()
    {
        inQuestion.text = allResults[questionIndex].question;
        GameObject.Find("C1").GetComponentInChildren<Text>().text = allResults[questionIndex].op1;
        GameObject.Find("C2").GetComponentInChildren<Text>().text = allResults[questionIndex].op2;
        GameObject.Find("C3").GetComponentInChildren<Text>().text = allResults[questionIndex].op3;
        GameObject.Find("C4").GetComponentInChildren<Text>().text = allResults[questionIndex].op4;

        
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://172.21.148.163:3381/pvpRetrieveQuestion.php"))
        {
            // Request and wait for the desired page.
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Successful and returns the php results as a JSON Array

                string results = www.downloadHandler.text;
                allResults = JsonHelper.GetArray<Questions>(results);
                questionIndex = PlayerPrefs.GetInt("qIndex");
                DrawUI();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}