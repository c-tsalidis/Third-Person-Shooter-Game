﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class ParticleSystemDieEffect : MonoBehaviour
    {
        #region Private non-serializefield variables
            
        #endregion

        #region Private serializeField variables

        #endregion

        #region Public variables

        public WaitForSeconds lifespanTime = new WaitForSeconds(1.5f);

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
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
            yield return lifespanTime;
            Destroy(this.gameObject);
        }


        #endregion
    }
}
