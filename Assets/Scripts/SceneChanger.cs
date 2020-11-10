using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string newlevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        SceneManager.LoadScene(newlevel);
    }
}
