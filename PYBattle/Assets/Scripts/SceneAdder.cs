using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneAdder : MonoBehaviour
{
    [SerializeField] private string newlevel;
    public GameObject sprite;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
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
