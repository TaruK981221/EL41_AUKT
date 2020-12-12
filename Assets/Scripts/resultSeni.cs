using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resultSeni : MonoBehaviour
{
    string resultScene = "Result";

    BallManager ballMana;

    float time;

    GameObject blocks;

    // Start is called before the first frame update
    void Awake()
    {
        ballMana = GameObject.Find("BallManager").GetComponent<BallManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(ballMana.numBalls < 1 && time >= 1.0f)
        {
            Result();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Result();
        }
    }
    
    void Result()
    {
        SceneManager.LoadScene(resultScene);
    }
}
