using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{
    public GameObject Char1;
    public GameObject Char2;
    public GameObject Char3;

    private Vector3 CharPos;
    private Vector3 OffscreenPos;

    private int CharInt = 1;
    private SpriteRenderer Char1Render, Char2Render, Char3Render;

    private void Awake()
    {
        CharPos = Char1.transform.position;
        OffscreenPos = Char2.transform.position;
        Char1Render = Char1.GetComponent<SpriteRenderer>();
        Char2Render = Char2.GetComponent<SpriteRenderer>();
        Char3Render = Char2.GetComponent<SpriteRenderer>();
    }

    public void NextCharBtn()
    {
        switch (CharInt)
         {
             case 1:
                 Char1Render.enabled = false;
                 Char1.transform.position = OffscreenPos;
                 Char2.transform.position = CharPos;
                 Char2Render.enabled = true;
                 CharInt++;  
                 break;
             case 2:
                Char2Render.enabled = false;
                Char2.transform.position = OffscreenPos;
                Char3.transform.position = CharPos;
                Char3Render.enabled = true;
                CharInt++;
                break;
            case 3:
                Char3Render.enabled = false;
                Char3.transform.position = OffscreenPos;
                Char1.transform.position = CharPos;
                Char1Render.enabled = true;
                CharInt++;
                ResetInt();
                break;
            default:
                ResetInt();
                break;
         }
    }

    public void PrevCharBtn()
    {
        switch (CharInt)
        {
            case 1:
                Char1Render.enabled = false;
                Char1.transform.position = OffscreenPos;
                Char3.transform.position = CharPos;
                Char3Render.enabled = true;
                CharInt--;
                ResetInt();
                break;
            case 2:
                Char2Render.enabled = false;
                Char2.transform.position = OffscreenPos;
                Char1.transform.position = CharPos;
                Char1Render.enabled = true;
                CharInt--;
                break;
            case 3:
                Char3Render.enabled = false;
                Char3.transform.position = OffscreenPos;
                Char2.transform.position = CharPos;
                Char2Render.enabled = true;
                CharInt--;
                break;
            default:
                ResetInt();
                break;
        }
    }

    private void ResetInt()
    {
        if (CharInt >= 3)
        {
            CharInt = 1;
        }
        else
        {
            CharInt = 3;
        }
    }

}
