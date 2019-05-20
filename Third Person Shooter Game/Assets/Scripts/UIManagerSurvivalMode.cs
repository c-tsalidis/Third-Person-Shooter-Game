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

        #endregion

        #region Public variables

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            float score = PlayerPrefs.GetFloat("Survival_Mode_Player_Score");
            scoreText.text = score.ToString();
            healthText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>().health.ToString();
        }

        #endregion

        #region Custom methods

        #endregion
    }
}
