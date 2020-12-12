using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kajitani
{
    public class UIBlock : MonoBehaviour
    {
        public float power = 10;
        public void Click()
        {
            
            foreach (UIBlock go in GameObject.FindObjectsOfType(typeof(UIBlock)))
            {
                go.Barn();
            }
        }
        void Barn()
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            Vector3 vec = transform.position;
            vec.z = 0;
            rb.velocity = vec.normalized*power;// new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 2f), 0) * 10f;
            rb.angularVelocity = new Vector3(0, 0, Random.Range(-100f, 100f));
            Destroy(this);
        }
    }
}