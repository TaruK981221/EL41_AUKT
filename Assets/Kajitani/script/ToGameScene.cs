using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kajitani
{
    public class ToGameScene : MonoBehaviour
    {
        public string[] names;
        static List<string> l_names = new List<string>();

        bool fst = true;

        public void OnClick()
        {
            if (fst)
            {
                Invoke("Action", 1.5f);
                fst = false;
            }
        }
        private void Action()
        {
            if(l_names.Count == 0){
                for (int i = 0; i < names.Length; i++)
                {
                    l_names.Add(names[i]);
                }
            }
            int r = Random.Range(0, l_names.Count);
            SceneManager.LoadScene(l_names[r]);
            l_names.RemoveAt(r);
        }
    }
}