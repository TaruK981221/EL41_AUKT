using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    bool isWall = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isWall)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position +=
                new Vector3(-speed, 0);

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position +=
                new Vector3(speed, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            isWall = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.transform.position.x);
            Debug.Log(this.transform.position.x);

            if (Input.GetKey(KeyCode.LeftArrow) &&
                collision.transform.position.x >= 
                this.transform.position.x)
            {
                this.transform.position +=
                new Vector3(-speed, 0);

            }

            if (Input.GetKey(KeyCode.RightArrow) &&
                collision.transform.position.x <=
                this.transform.position.x)
            {
                this.transform.position +=
                new Vector3(speed, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isWall = false;
        }
    }
}
