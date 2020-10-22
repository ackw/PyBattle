using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPVP : MonoBehaviour
{
    public Text player1Score_text;
    public Text player2Score_text;
    public Text player1Name_text;
    public Text player2Name_text;
    private int player1_score;
    private int player2_score;
    private string player1_name;
    private string player2_name;

    // Use this for initialization
    void Start()
    {
        player1_score = PlayerPrefs.GetInt("player1_score");
        player2_score = PlayerPrefs.GetInt("player2_score");
        player1_name = PlayerPrefs.GetString("player1_name");
        player2_name = PlayerPrefs.GetString("player2_name");

        player1Score_text.text = "Score: " + player1_score.ToString();
        player2Score_text.text = "Score: " + player2_score.ToString();
        player1Name_text.text = "Name: " + player1_name;
        player2Name_text.text = "Name: " + player2_name;
    }
}
