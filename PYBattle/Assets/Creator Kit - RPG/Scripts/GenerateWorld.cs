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

    private SpriteRenderer Cave1Render, Cave2Render, Cave3Render, Cave4Render, Cave5Render;
    public List<SpriteRenderer> caveArray;

    void Awake()
    {
        Cave1Render = Cave1.GetComponent<SpriteRenderer>();
        Cave2Render = Cave2.GetComponent<SpriteRenderer>();
        Cave3Render = Cave3.GetComponent<SpriteRenderer>();
        Cave4Render = Cave4.GetComponent<SpriteRenderer>();
        Cave5Render = Cave5.GetComponent<SpriteRenderer>();

        caveArray.Add(Cave1Render);
        caveArray.Add(Cave2Render);
        caveArray.Add(Cave3Render);
        caveArray.Add(Cave4Render);
        caveArray.Add(Cave5Render);

        for (int i = 0; i < caveArray.Count; i++)
        {
            caveArray[i].enabled = false;
        }

        

        int num = 3;

        for (int i=0; i < num; i++)
        {
            caveArray[i].enabled = true;
        }


    }


}
