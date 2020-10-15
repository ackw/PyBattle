using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMainChar : MonoBehaviour
{
    public Sprite Char1, Char2, Char3;
    private SpriteRenderer mySprite;
    private readonly string SelectedChar = "SelectedCharacter";

    void Awake()
    {
        mySprite = this.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        int getCharacter;
        getCharacter = PlayerPrefs.GetInt(SelectedChar,1);

        switch (getCharacter)
        {
            case 1:
                mySprite.sprite = Char1;
                break;
            case 2:
                mySprite.sprite = Char2;
                break;
            case 3:
                mySprite.sprite = Char3;
                break;
            default:
                mySprite.sprite = Char1;
                break;
        }
        
    }

}
