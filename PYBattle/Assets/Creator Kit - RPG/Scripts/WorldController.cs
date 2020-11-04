using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using TMPro;

public class WorldController : MonoBehaviour
{
    //private readonly string World;

    private string worldName;

    void OnTriggerEnter2D(Collider2D other)
    {
        print("collision detected");
        if (other.CompareTag("player"))
        {
            GetSection();
            //SceneManager.LoadScene("World");
        }
    }

    private void GetSection()
    {
        print("get section");
        
        worldName = gameObject.name;

        PlayerPrefs.SetString("worldName", worldName);
        SceneManager.LoadScene("World");
    }

    //Generate world codes-------------------------

    

}
