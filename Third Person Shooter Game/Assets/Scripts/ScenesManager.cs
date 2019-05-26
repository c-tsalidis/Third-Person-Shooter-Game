using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class ScenesManager : MonoBehaviour
    {
        // loads the scene with the name sceneName
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
