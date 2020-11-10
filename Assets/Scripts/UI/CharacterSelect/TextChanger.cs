using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanging : MonoBehaviour
{
    private TextMeshProUGUI uiText;

    void StartTextChanger()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        uiText.text = "Select Character";
    }

}
