using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [Serializable]
    public struct Leaderboard
    {
        public string userName;
        public string score;
    }

    public Button returnButton;

    Leaderboard[] allResults;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(GetData());

        returnButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameSelection");
        });
    }

    void DrawUI()
    {
        GameObject phTemplate = transform.GetChild(0).gameObject;
        GameObject g;   //Keep track of new created items

        int size = allResults.Length;

        if (size == 0)
        {
            phTemplate.transform.GetChild(1).GetComponent<Text>().text = "No data in leaderboard yet!";
            phTemplate.transform.GetChild(1).GetComponent<Text>().color = Color.red;

        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                g = Instantiate(phTemplate, transform);
                g.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                g.transform.GetChild(1).GetComponent<Text>().text = allResults[i].userName;
                g.transform.GetChild(2).GetComponent<Text>().text = allResults[i].score;
            }

            Destroy(phTemplate);    //Destroy Item Placeholder
        }
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://172.21.148.163:3381/loadleaderboard.php"))
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
                allResults = JsonHelper.GetArray<Leaderboard>(results);
                DrawUI();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}