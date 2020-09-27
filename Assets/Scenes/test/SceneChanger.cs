using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger: MonoBehaviour {

    public void Home() {
      SceneManager.LoadScene("Home");
    }

    public void Login() {
        SceneManager.LoadScene("Login");
    }

    public void SelectMode() {
        SceneManager.LoadScene("SelectMode");
    }

    public void Game() {
        SceneManager.LoadScene("Game");
    }
    
    public void Question() {
        SceneManager.LoadScene("Question");
    }

    public void PVP() {
        SceneManager.LoadScene("PVP");
    }
}
