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
        StartCoroutine(GetData());
    }

    void Sections()
    {
        print("printing section names");

        for (int i = 0; i < num; i++)
        {
            if (allResults[i].world == PlayerPrefs.GetString("worldName"))
            {
                sectionName = allResults[i].section;
            }
        }

        SectionText.text = sectionName;

    }

    IEnumerator GetData()
    {
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

                for (int i = 0; i < allResults.Length; i++)
                {
                    print(allResults[i].world + " section is " + allResults[i].section);
                }

                num = allResults.Length;

                Sections();
            }
        }
    }
}