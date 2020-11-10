using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{

    static int score;
    [Serializable]
    public struct Questions
    {
        public string question;
        public int level;
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
    public int clickedcount = 0;
    public int level = 0;
    static int clearedQns = 0;

    int getDifficulty(int score, int level)
    {
        switch (this.level)
        {
            case 1:
                if (score>4)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 2:
                if (score > 8)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 3:
                if (score > 15)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }

    }

    void getQuestions(int level)
    {
        for(int i = 0; i < 19; i++)
        {
            if(allResults[i].level == level)
            {
                questionIndex = i+clearedQns+(getDifficulty(score, level)*3);

                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        questionIndex = 0;
        PlayerPrefs.SetInt("qIndex", 0);

        StartCoroutine(GetData());
        in1.onClick.AddListener(() =>
        {
            if(string.Equals("1", allResults[questionIndex].correct_ans))
            {
                print("Correct Ans");
                questionIndex += 1;
                PlayerPrefs.SetInt("qIndex", questionIndex);
                SceneManager.UnloadSceneAsync("Dialog");
                Time.timeScale = 1f;
                score += 1;
                print(score);

            }
            else
            {
                print("Wrong Ans");
                clickedcount += 1; 
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
                Time.timeScale = 1f;
                score += 1;
                print(score);

            }
            else
            {
                print("Wrong Ans");
                clickedcount += 1;

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
                Time.timeScale = 1f;
                score += 1;
                print(score);

            }
            else
            {
                print("Wrong Ans");
                clickedcount += 1;

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
                Time.timeScale = 1f;
                score += 1;
                print(score);

            }
            else
            {
                print("Wrong Ans");
                clickedcount += 1;

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

    public int getScores()
    {
        return score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("level 1"))
            print("Level 1");
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://172.21.148.163:3381/loadquestion.php"))
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
                getQuestions(2);
                clearedQns += 1;
                level = PlayerPrefs.GetInt("level");
                print(level);
                DrawUI();
                print(questionIndex);
            }
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}