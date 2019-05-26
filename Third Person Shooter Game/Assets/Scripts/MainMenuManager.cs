using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject instructionsPanel;
        [SerializeField]
        private GameObject creditsPanel;

        private bool instructionsPanelisActive = true;
        private bool creditsPanelisActive = true;


        private void Start()
        {
            Time.timeScale = 1;
        }


        public void PlaySurvivalMode()
        {
            SceneManager.LoadScene("Survival Mode");
        }
        public void Instructions()
        {
            instructionsPanel.SetActive(instructionsPanelisActive);
            instructionsPanelisActive = !instructionsPanelisActive;
        }
        public void Credits()
        {
            creditsPanel.SetActive(creditsPanelisActive);
            creditsPanelisActive = !creditsPanelisActive;
        }
        public void Quit()
        {
            // for quitting the entire program
            Application.Quit();
        }


        public void LoadUrl(string url)
        {
            Debug.Log("Loading url: " + url);
            Application.OpenURL(url);
        }

    }
}
