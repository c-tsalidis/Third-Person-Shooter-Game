using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class LivingEntity : MonoBehaviour
    {
        #region Private non-serializefield variables

        // private GameObject player;
        private Color increaseHealthStartColor;
        private Color takingDamageStartColor;

        #endregion

        #region Private serializeField variables

        [SerializeField]
        private GameObject visuals;
        [SerializeField]
        private ParticleSystem dieEffectPS;
        [SerializeField]
        private GameObject increaseHealthPanel;
        [SerializeField]
        private GameObject takingDamagePanel;

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
            if(this.gameObject.tag == "Player")
            {
                increaseHealthStartColor = increaseHealthPanel.GetComponent<Image>().color;
                takingDamageStartColor = takingDamagePanel.GetComponent<Image>().color;
            }
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
                // GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                if(player != null)
                {
                    if(this.gameObject.tag == "Enemy")
                    {
                        // the reason the score is being added by more than just one is because
                        //  the gameobject is dead until it is destroyed in the IEnumerator
                        player.GetComponent<Player>().score++;
                    }
                }
                /*
                if(gameManager != null)
                {
                    gameManager.GetComponent<GameManager>().enemyCounter++;
                }
                 */
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
            takingDamagePanel.SetActive(true);
            // takingDamagePanel.GetComponent<Image>().color = increaseHealthPanel.GetComponent<Image>().color;
            takingDamagePanel.GetComponent<Image>().color = takingDamageStartColor;
        }

        // IEnumerator Die()
        private void Die()
        {
            visuals.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            if(dieEffectPS != null)
            {
                ParticleSystem newDieEffectPS = Instantiate(dieEffectPS, this.gameObject.transform.position, Quaternion.identity) as ParticleSystem;
                newDieEffectPS.Play();
            }
            // yield return dieTime;
            Destroy(this.gameObject);
        }


        public void IncreaseHealth(float addedHealth)
        {
            health += addedHealth;
            increaseHealthPanel.SetActive(true);
            // increaseHealthPanel.GetComponent<Image>().color = increaseHealthPanel.GetComponent<Image>().color;
            increaseHealthPanel.GetComponent<Image>().color = increaseHealthStartColor;
        }


        #endregion
    }
}
