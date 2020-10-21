using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<SpriteRenderer> caveArray;
    public List<MeshRenderer> textArray;
    public List<GameObject> caveList;
    public List<GameObject> textList;

    void Awake()
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
            caveArray.Add(caveList[i].GetComponent<SpriteRenderer>());
            textArray.Add(textList[i].GetComponent<MeshRenderer>());
            caveList[i].GetComponent<BoxCollider2D>().enabled = false;
            caveArray[i].enabled = false;
            textArray[i].enabled = false;
        }

        int num = 3;

        for (int i = 0; i < num; i++)
        {
            caveArray[i].enabled = true;
            caveList[i].GetComponent<BoxCollider2D>().enabled = true;
            textArray[i].enabled = true;
        }

    }


}