using System;
using System.Collections;
using System.Runtime.InteropServices;
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
    static int qnscounter = 1;
    static int isboss = 0;

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
                if (score > 10)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            case 3:
                if (score > 18)
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
        for(int i = 0; i < allResults.Length-2; i++)
        {
            if (level != 4)
            {
                if (allResults[i].level == level)
                {
                    questionIndex = i + qnscounter - 1 + (getDifficulty(score, level) * 3);
                   
                    break;
                }
            }
            else
            {
                questionIndex = allResults.Length-1;

                break;
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        questionIndex = 0;
        PlayerPrefs.SetInt("qIndex", 0);
        int newlevel = 4;
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
                if(clickedcount<4)
                score += 3-clickedcount;

                clickedcount = 0;
                print(score);

                if (level == 4)
                {
                    //post
                    StopCoroutine(GetData());
                    StartCoroutine(PostScores());
                }
                else
                {
                    if (qnscounter < 3)
                    {
                        qnscounter++;
                        SceneManager.LoadScene(newlevel, LoadSceneMode.Additive);
                    }
                    else
                    {
                        qnscounter = 1;
                    }
                }

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
                if (clickedcount < 4)
                    score += 3 - clickedcount;

                clickedcount = 0;
                print(score);

                if (level == 4)
                {
                    //post
                    StopCoroutine(GetData());

                    StartCoroutine(PostScores());
                }
                else
                {
                    if (qnscounter < 3)
                    {
                        qnscounter++;
                        SceneManager.LoadScene(newlevel, LoadSceneMode.Additive);
                    }
                    else
                    {
                        qnscounter = 1;
                    }
                }

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
                if (clickedcount < 4)
                    score += 3 - clickedcount;

                clickedcount = 0;
                print(score);

                if (level == 4)
                {
                    //post
                    StopCoroutine(GetData());

                    StartCoroutine(PostScores());
                }
                else
                {
                    if (qnscounter < 3)
                    {
                        qnscounter++;
                        SceneManager.LoadScene(newlevel, LoadSceneMode.Additive);
                    }
                    else
                    {
                        qnscounter = 1;
                    }
                }

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
                if (clickedcount < 4)
                    score += 3 - clickedcount;

                clickedcount = 0;
                print(score);
                if(level == 4)
                {
                    //post
                    StopCoroutine(GetData());

                    StartCoroutine(PostScores());
                }
                else
                {
                    if (qnscounter < 3)
                    {
                        qnscounter++;
                        SceneManager.LoadScene(newlevel, LoadSceneMode.Additive);
                    }
                    else
                    {
                        qnscounter = 1;
                    }
                }


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

    IEnumerator GetData()
    {
        WWWForm form = new WWWForm();
        form.AddField("section", PlayerPrefs.GetString("sectionName"));
        form.AddField("worldName", PlayerPrefs.GetString("worldName"));
        using (UnityWebRequest www = UnityWebRequest.Post("http://172.21.148.163:3381/loadquestion.php", form))
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
                level = PlayerPrefs.GetInt("level");
                getQuestions(level);
                clearedQns += 1;
                print(level);
                DrawUI();
                print("this is q index" + questionIndex);
                print("Section Name");
                print(PlayerPrefs.GetString("sectionName"));
                print("World Name");
                print("all result here ---------------------------");
                print(results);

            }
        }
    }

    IEnumerator PostScores()
    {

        print("Enter here");

        string userID = PlayerPrefs.GetString("userKey");
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("user", userID);
        scoreForm.AddField("score", score);
        scoreForm.AddField("section", PlayerPrefs.GetString("sectionName"));
        scoreForm
            .AddField("worldName", PlayerPrefs.GetString("worldName"));


        using (UnityWebRequest www = UnityWebRequest.Post("http://172.21.148.163:3381/submitplayerscore.php", scoreForm))
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
                print(results);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}