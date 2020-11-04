using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GenerateWorld : MonoBehaviour
{
    public GameObject Cave1;
    public GameObject Cave2;
    public GameObject Cave3;
    public GameObject Cave4;
    public GameObject Cave5;

    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject Text5;

    int num = 0;

    public List<GameObject> caveList;
    public List<GameObject> textList;
    public List<SpriteRenderer> caveRenderList;
    public List<MeshRenderer> textRenderList;

    [Serializable]
    public struct WorldData
    {
        public string world_name;
    }

    WorldData[] allResults;

    void Awake()
    {
        StartCoroutine(GetData());
    }

    void DrawUI()
    {
        caveList.Add(Cave1);
        caveList.Add(Cave2);
        caveList.Add(Cave3);
        caveList.Add(Cave4);
        caveList.Add(Cave5);

        textList.Add(Text1);
        textList.Add(Text2);
        textList.Add(Text3);
        textList.Add(Text4);
        textList.Add(Text5);

        for (int i = 0; i < caveList.Count; i++)
        {
            caveRenderList.Add(caveList[i].GetComponent<SpriteRenderer>());
            textRenderList.Add(textList[i].GetComponent<MeshRenderer>());

            caveRenderList[i].enabled = false;
            textRenderList[i].enabled = false;

            caveList[i].GetComponent<BoxCollider2D>().enabled = false;


        }

        int caveNum = num; //replace with database no. of worlds
        print(caveNum);

        for (int i = 0; i < caveNum; i++)
        {
            caveRenderList[i].enabled = true;
            textRenderList[i].enabled = true;
            caveList[i].GetComponent<BoxCollider2D>().enabled = true;
            textList[i].GetComponent<TextMeshPro>().text = allResults[i].world_name;
        }
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://172.21.148.163:3381/loadworld.php"))
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
                allResults = JsonHelper.GetArray<WorldData>(results);
                print(allResults.Length);
                num = allResults.Length;
            }

            DrawUI();
        }
    }
}