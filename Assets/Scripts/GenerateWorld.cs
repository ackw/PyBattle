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

    public Sprite cavelock;

    private int numWorlds = 0;
    private int numCompleted = 0;

    public List<GameObject> caveList;
    public List<GameObject> textList;
    public List<SpriteRenderer> caveRenderList;
    public List<MeshRenderer> textRenderList;
    private string[] world_name;
    private string[] uncompleted_name;

    [Serializable]
    public struct WorldData
    {
        public int Total_No_Of_World;
        public string List_Of_World;
        public int No_Of_Completed_World;
        public string List_Of_Completed_World;
        public int No_Of_Uncompleted_World;
        public string List_Of_Uncompleted_World;
    }
    WorldData[] allResults;

    void Awake()
    {
        print("start");
        StartCoroutine(GetData());
        //DrawUI();
    }

    void DrawUI()
    {
        print("generate world - draw UI");
        
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

        //dummy to test
        /*numWorlds = 5;
        numCompleted = 2;
        string worlds = "one,two,three,four,five";
        world_name = worlds.Split(char.Parse(","));*/

        print("numWorlds = " + numWorlds); //database no. of worlds

        for (int i = 0; i < numWorlds; i++)
        {
            caveRenderList[i].enabled = true;
            textRenderList[i].enabled = true;
            caveList[i].GetComponent<BoxCollider2D>().enabled = true;

            textList[i].GetComponent<TextMeshPro>().text = world_name[i];
            caveList[i].name = world_name[i];
        }

        for (int i=0; i < numWorlds; i++) //add locks, disable collider for uncompleted caves
        {
            if (i != numCompleted) //currently disable collider for all except current (or will have issues generating section name)
            {
                caveList[i].GetComponent<BoxCollider2D>().enabled = false;
            }
            if (i > numCompleted)
            {
                caveList[i].GetComponent<SpriteRenderer>().sprite = cavelock;
            }
        }
    }

    IEnumerator GetData()
    {
        print("trying to get data");

        string userID = PlayerPrefs.GetString("userKey");
        userID = "zen"; //hardcode
        WWWForm form = new WWWForm();
        form.AddField("user", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://172.21.148.163:3381/loadplayerworld.php",form))
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

                print("data obtained");
                print(results);

                //total number of worlds
                numWorlds = allResults[0].Total_No_Of_World;
                //number of completed worlds
                numCompleted = allResults[0].No_Of_Completed_World;
                //generate list of world names
                //world_name = allResults[0].List_Of_World.Split(char.Parse(","));
                world_name = allResults[0].List_Of_World.Split(new string[] {", "}, StringSplitOptions.None);
                uncompleted_name = allResults[0].List_Of_Uncompleted_World.Split(new string[] { ", " }, StringSplitOptions.None);
                print("printing world name");
                print("world name:" + uncompleted_name[0]);

            }

            DrawUI();
        }
    }
}