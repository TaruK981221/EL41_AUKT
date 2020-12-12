using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMana : MonoBehaviour
{
    [SerializeField]
    GameObject textBox;

    Text text;

    int Score;

    bool isAdd = false;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;

        text = textBox.GetComponent<Text>();

        text.text = "Score:" + Score;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAdd)
        {
            isAdd = false;

            text.text = "Score:" + Score;
        }
    }

    public void AddScore(int value)
    {
        Score += value;

        isAdd = true;
    }
}
