using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Death : MonoBehaviour
{
    GameObject ScoreAdd;

    [SerializeField]
    int addScore = 100;

    [SerializeField]
    List<GameObject> Hidekis;

    // Start is called before the first frame update
    void Start()
    {
        ScoreAdd = GameObject.Find("ScoreManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ScoreAdd.GetComponent<SceneMana>().AddScore(addScore);

            int rand = Random.Range(0, Hidekis.Count + 2);
            if (rand >= Hidekis.Count) rand = 0;

            // Hideki生成
            Instantiate(
                Hidekis[rand],
                collision.transform.position,
                Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
