using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneAdder : MonoBehaviour
{
    [SerializeField] private string newlevel;
    public GameObject sprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        PlayerPrefs.SetInt("level", Int32.Parse(sprite.tag));
        PlayerPrefs.SetInt("qnsCount", 1);
        SceneManager.LoadScene(newlevel, LoadSceneMode.Additive);

        removeSprite();
        Time.timeScale = 0.0f;

    }
    public void removeSprite()
    {
        sprite.GetComponent<SpriteRenderer>().enabled = false;
        sprite.GetComponent<BoxCollider2D>().enabled = false;

        Destroy(sprite);
        //return;
    }

   
}
