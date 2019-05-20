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
        private Vector3 lineOrigin;
        private Vector3 rayOrigin;
        private RaycastHit hit;
            
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
        private float msBetweenShots = 2000f;
        [SerializeField]
        private float weaponRange = 1000f;
        [SerializeField]
        private float hitForce = 100f;

        
        #endregion

        #region Public variables

        public bool isEquiped = false;
        public float nextShotTime = 0.0f;

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
            lineOrigin = player.transform.position;
            rayOrigin = player.transform.position;
            // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
            Debug.DrawRay(lineOrigin, player.transform.forward * weaponRange, Color.green);
            if(Physics.Raycast(rayOrigin, player.transform.forward, out hit, weaponRange))
            {
                if(hit.rigidbody != null)
                {
                    // if(hit.rigidbody.tag == "Enemy" || hit.rigidbody.tag == "Player")
                    {
                        // Debug.Log("Pointing at " + hit.rigidbody.name);
                    }
                }
            }
        }


        #endregion

        #region Custom methods

        public void Shoot()
        {
            if(Time.time > nextShotTime)
            {
                nextShotTime = Time.time + msBetweenShots / 1000;
                Debug.Log("Shot");
                StartCoroutine(ShotEffect());
                // ray
                lineOrigin = player.transform.position;
                rayOrigin = player.transform.position;
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
