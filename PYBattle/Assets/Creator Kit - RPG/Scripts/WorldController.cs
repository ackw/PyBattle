using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldController : MonoBehaviour
{
    //private readonly string World;

    private string worldName;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            GetSection();
        }
        /*if (other.CompareTag("player"))
        {
            if (gameObject.name == "Cave3")
            {
                SceneManager.LoadScene("World");
            }
        }*/ //sample on how to use gameObject.name
        
    }

    private void GetSection()
    {
        worldName = gameObject.name;
        //pass worldName into database to retrieve section, levels data

        //dummy data: (pretend that data has been obtained)
        int sectionNum = 2;
        
        //pass the world/section info back into database
        //when re-entering the worlds, check for the section level to enter

        SceneManager.LoadScene("World");

    }

}
