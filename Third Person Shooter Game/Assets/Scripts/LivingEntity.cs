using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class LivingEntity : MonoBehaviour
    {
        #region Private non-serializefield variables


        #endregion

        #region Private serializeField variables

        [SerializeField]
        private GameObject visuals;
        [SerializeField]
        private ParticleSystem dieEffectPS;

        #endregion

        #region Public variables

        public float health = 10f;
        public bool isDead = false;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            // dieEffectPS = this.gameObject.GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if(health <= 0f)
            {
                isDead = true;
            }
            if(isDead == true)
            {
                StartCoroutine(Die());
            }
        }

        #endregion

        #region Custom methods


        public void TakeDamage(float damage)
        {
            health -= damage;
        }

        IEnumerator Die()
        {
            visuals.SetActive(false);
            if(dieEffectPS != null)
            {
                dieEffectPS.Play();
            }
            yield return new WaitForSeconds(1.5f);
            Destroy(this.gameObject);
        }


        #endregion
    }
}
