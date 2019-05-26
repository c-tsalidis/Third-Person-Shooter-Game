using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class HealthPanelManager : MonoBehaviour
    {
        #region Private non-serializefield variables
        private Image flashImage;
        
        #endregion

        #region Private serializeField variables
        [SerializeField]
        private float flashSpeed = 5f;
        #endregion

        #region Public variables

        // public Transform canvasParent;
        public WaitForSeconds lifeSpan = new WaitForSeconds(0.001f);

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            // this.transform.SetParent(canvasParent);
            // StartCoroutine(Die());
            flashImage = this.gameObject.GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            flashImage.color = Color.Lerp (flashImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        #endregion

        #region Custom methods

        IEnumerator Die()
        {
            yield return lifeSpan;
            // Destroy(this.gameObject);
            // this.gameObject.SetActive(false);
        }

        #endregion
    }
}
