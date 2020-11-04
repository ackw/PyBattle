using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{

    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;

    string uIn;
    string pIn;

    // Start is called before the first frame update
    void Start()
    {
        usernameInput.Select();

        loginButton.onClick.AddListener(() =>
        {
            authenticate();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameInput.isFocused)
            {
                passwordInput.Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            authenticate();
        }
    }

    public void authenticate()
    {
        uIn = usernameInput.text;
        pIn = passwordInput.text;

        if (uIn != "" && pIn != "")
        {
            StartCoroutine(GetData(uIn, pIn));
        }
        else
        {
            print("Please fill in the blanks!");
        }
    }

    IEnumerator GetData(string u, string p)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", u);
        form.AddField("password", p);

        using (UnityWebRequest www = UnityWebRequest.Post("http://172.21.148.163:3381/login.php", form))
        {
            // Request and wait for the desired page.
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string results = www.downloadHandler.text;
                if (string.Equals(results, "success"))
                {
                    print("Login Success!");
                    SceneManager.LoadScene("GameSelection");
                }
                else
                {
                    print(results);
                }
            }
        }
    }
}
