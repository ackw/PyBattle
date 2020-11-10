using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Sprite sprite;


    public List<string> names = new List<string>() { "C1", "C2", "C3" };
    void Start()
    {

        for (int i = 1; i < 3; i++)
        {
            GameObject go = new GameObject(names[i]);
        go.transform.position = new Vector3(0.067f*i, 1.52f*i, -0.37f);
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
        }
    }

}
