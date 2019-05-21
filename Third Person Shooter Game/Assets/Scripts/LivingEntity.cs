using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class LivingEntity : MonoBehaviour
    {
        #region Private non-serializefield variables

        // private GameObject player;

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

        public WaitForSeconds dieTime = new WaitForSeconds(1.5f);
        public AudioSource takeDamageSound;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            // player = GameObject.FindGameObjectWithTag("Player");
            // dieEffectPS = this.gameObject.GetComponent<ParticleSystem>();
            // audioSource[0] = GetComponents<AudioSource>();
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
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(player != null)
                {
                    if(this.gameObject.tag == "Enemy")
                    {
                        // the reason the score is being added by more than just one is because
                        //  the gameobject is dead until it is destroyed in the IEnumerator
                        player.GetComponent<Player>().score++;
                    }
                }
                // StartCoroutine(Die());
                // StartCoroutine(Die());
                Die();
            }
        }

        #endregion

        #region Custom methods


        // TakeDamage for all LivingEntities except Player
        public void TakeDamage(float damage)
        {
            health -= damage;
        }

        // TakeDamage for Player
        public void TakeDamage(float damage, Vector3 hitPoint)
        {
            health -= damage;
            this.gameObject.GetComponent<Player>().ShowTakingDamagePanel(hitPoint);
            takeDamageSound.Play();
        }

        // IEnumerator Die()
        private void Die()
        {
            visuals.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            if(dieEffectPS != null)
            {
                ParticleSystem newDieEffectPS = Instantiate(dieEffectPS, dieEffectPS.transform.position, Quaternion.identity) as ParticleSystem;
                newDieEffectPS.Play();
            }
            // yield return dieTime;
            Destroy(this.gameObject);
        }


        public void IncreaseHealth(float addedHealth)
        {
            health += addedHealth;
        }


        #endregion
    }
}
