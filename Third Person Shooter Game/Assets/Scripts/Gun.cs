using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(AudioSource))]
    public class Gun : MonoBehaviour
    {
        #region Private non-serializefield variables
        private Camera mainCamera;
        private LineRenderer laserLine;
        private AudioSource gunAudio;
        private float nextFire;
            
        #endregion

        #region Private serializeField variables

        [SerializeField]
        private GameObject player;
        [SerializeField]
        private WaitForSeconds shotDuration = new WaitForSeconds(0.005f);
        [SerializeField]
        private Transform gunEnd;
        [SerializeField]
        private float gunDamage = 1f;
        [SerializeField]
        private float fireRate = 0.25f;
        [SerializeField]
        private float weaponRange = 50f;
        [SerializeField]
        private float hitForce = 100f;
        
        
        #endregion

        #region Public variables

        public bool isEquiped = false;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
            laserLine = this.gameObject.GetComponent<LineRenderer>();
            gunAudio = this.gameObject.GetComponent<AudioSource>();

            laserLine.enabled = false;
        }

        private void Update()
        {
            
        }


        #endregion

        #region Custom methods

        public void Shoot()
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            // ray
            Vector3 lineOrigin = player.transform.position;
            Vector3 rayOrigin = player.transform.position;
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if(Physics.Raycast(rayOrigin, player.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point); 

                // ShootableObject shootableObject = hit.collider.GetComponent<ShootableObject>();
                LivingEntity LivingEntity = hit.collider.GetComponent<LivingEntity>();
                if(LivingEntity != null)
                {
                    LivingEntity.TakeDamage(gunDamage);
                }
                if(hit.rigidbody != null)
                {
                    Debug.Log("Hit " + hit.rigidbody.name);
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (player.transform.forward * weaponRange));
            }

        }

        IEnumerator ShotEffect()
        {
            laserLine.enabled = true;
            if(gunAudio != null)
            {
                gunAudio.Play();
            }
            yield return shotDuration;
            laserLine.enabled = false;
        }

        #endregion
    }
}
