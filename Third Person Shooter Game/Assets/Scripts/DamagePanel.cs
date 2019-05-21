using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class DamagePanel : MonoBehaviour
    {
        #region Private non-serializefield variables
            
        #endregion

        #region Private serializeField variables

        #endregion

        #region Public variables

        public Transform canvasParent;
        public WaitForSeconds lifeSpan = new WaitForSeconds(0.5f);

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            this.transform.SetParent(canvasParent);
            StartCoroutine(Die());
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        #endregion

        #region Custom methods

        IEnumerator Die()
        {
            yield return lifeSpan;
            Destroy(this.gameObject);
        }

        #endregion
    }
}
