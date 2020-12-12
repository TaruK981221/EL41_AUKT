using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kajitani
{
    public class ButtonSceneChange : MonoBehaviour
    {
        public string sceneName;
        public void SceneChange()
        {
            Invoke("Action", 1f);
        }
        public void SceneChange(string l_sceneName)
        {
         sceneName=   l_sceneName;
            Invoke("Action", 1f);
        }
        private void Action()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}