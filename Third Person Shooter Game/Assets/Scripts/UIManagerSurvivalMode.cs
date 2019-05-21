using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class UIManagerSurvivalMode : MonoBehaviour
    {
        #region Private non-serializefield variables

        #endregion

        #region Private serializeField variables

        [SerializeField]
        private TextMeshProUGUI healthText;
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI highScoreText;

        #endregion

        #region Public variables

        public GameObject gameOverPanel;
        public TextMeshProUGUI levelText;
        public GameManager gameManager;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // float score = PlayerPrefs.GetFloat("Survival_Mode_Player_Score");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                healthText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>().health.ToString();
                float score = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().score;
                scoreText.text = score.ToString();
                
                if(player.GetComponent<LivingEntity>().isDead == true)
                {
                    // Debug.Log("Game Over!");
                    gameOverPanel.SetActive(true);
                }
            }
            float level = gameManager.enemyCounter;
            levelText.text = level.ToString();

            float highScore = PlayerPrefs.GetFloat("SurvivalMode_Player_HighScore");
            highScoreText.text = "Highscore: " + highScore.ToString("0");
        }

        #endregion

        #region Custom methods

        #endregion
    }
}
