using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class PowerUp : MonoBehaviour
    {
        #region Private non-serializefield variables
            
        #endregion

        #region Private serializeField variables

        [SerializeField]
        private float addedHealthValue = 3;

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
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }

        #endregion

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log(this.gameObject.name + " triggered with " + collider.gameObject.tag);
            if(collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<LivingEntity>().IncreaseHealth(addedHealthValue);
                Debug.Log(collider.tag + " received " + addedHealthValue + " health");
                Destroy(this.gameObject);
            }
        }

        #region Custom methods

        #endregion
    }
}
