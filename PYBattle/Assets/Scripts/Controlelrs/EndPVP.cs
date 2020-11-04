using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    public Button twittershare;
    public Button facebookshare;
    public Button endPVP;

    // Use this for initialization
    void Start()
    {
        player1_score = PlayerPrefs.GetInt("player1_score");
        player2_score = PlayerPrefs.GetInt("player2_score");
        player1_name = PlayerPrefs.GetString("player1_name");
        player2_name = PlayerPrefs.GetString("player2_name");

        player1Score_text.text = "Score: " + player1_score.ToString();
        player2Score_text.text = "Score: " + player2_score.ToString();
        player1Name_text.text = "Name: " + player1_name.ToString();
        player2Name_text.text = "Name: " + player2_name.ToString();


        /* TWITTER VARIABLES*/

        //Twitter Share Link
        string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";

        //Language
        string TWEET_LANGUAGE = "en";

        //This is the text which you want to show
        string textToDisplay="Hey Guys! Check out my PyBattle PvP Challenge Result: \n";

        string result= "Name: " + player1_name.ToString() + ". " + "Score: " + player1_score.ToString() + ". \n" + "Name: " + player2_name.ToString() +". "+ "Score: " + player2_score.ToString() + ". ";

        /*END OF TWITTER VARIABLES*/

        twittershare.onClick.AddListener( delegate {
            shareScoreOnTwitter(TWITTER_ADDRESS, TWEET_LANGUAGE, textToDisplay, result);
        } );

        facebookshare.onClick.AddListener( delegate {
            sharePostOnFacebook(textToDisplay, result);
        } );

        endPVP.onClick.AddListener(delegate {
            SceneManager.LoadScene("GameSelection");
        });
    }

        

    
    // Twitter Share Button	
        private void shareScoreOnTwitter (string TWITTER_ADDRESS, string TWEET_LANGUAGE, string textToDisplay, string result) 
        {
            Application.OpenURL (TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(textToDisplay) + WWW.EscapeURL(result) + "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
        }
    // share on facebook
        public void sharePostOnFacebook(string textToDisplay, string result)
        {
    
            Application.OpenURL("https://www.facebook.com/sharer/sharer.php?u=google.com&quote=" + WWW.EscapeURL(textToDisplay) + WWW.EscapeURL(result));
        }
    
}
