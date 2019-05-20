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
                /*
                if(player != null)
                {
                    if(this.gameObject.tag == "Enemy")
                    {
                        player.GetComponent<Player>().score++;
                    }
                }
                 */
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
            if(this.gameObject.transform.tag == "Player")
            {
                takeDamageSound.Play();
            }
        }

        IEnumerator Die()
        {
            visuals.SetActive(false);
            if(dieEffectPS != null)
            {
                dieEffectPS.Play();
            }
            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                if(this.gameObject.transform.tag == "Player")
                {
                    Debug.Log("Game Over!");
                }
                yield return new WaitForSeconds(1.5f);
                Destroy(this.gameObject);
            }
        }

        #endregion
    }
}
