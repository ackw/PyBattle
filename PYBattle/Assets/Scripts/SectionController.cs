using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using TMPro;

public class SectionController : MonoBehaviour
{
    private string sectionName;

    public TextMeshPro SectionText;

    int num = 0;

    [Serializable]
    public struct WorldProgress
    {
        public string world;
        public string section;
    }
    WorldProgress[] allResults;

    void Start()
    {
        print("starting section controller");
        StartCoroutine(GetData());
    }

    void Sections()
    {
        print(PlayerPrefs.GetString("worldName"));
        print(allResults[0].world);

        if (PlayerPrefs.GetString("worldName") == allResults[0].world) //changing text to section name
        {
            sectionName = allResults[0].section;
            PlayerPrefs.SetString("sectionName", sectionName);
        } //if enter a world that is not player's current world then can't generate section?

        SectionText.text = sectionName;

    }

    IEnumerator GetData()
    {
        string userID = PlayerPrefs.GetString("userKey");

        WWWForm form = new WWWForm();
        form.AddField("user", userID);

        using (UnityWebRequest www = UnityWebRequest.Get("http://172.21.148.163:3381/loadplayerprogress.php"))
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
                print("This is the results " + results);
                allResults = JsonHelper.GetArray<WorldProgress>(results);


                print(allResults[0].world + " section is " + allResults[0].section);

                Sections();
            }
        }
    }
}