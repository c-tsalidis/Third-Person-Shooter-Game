using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class Enemy : MonoBehaviour
    {
        #region Private non-serializefield variables
        private NavMeshAgent agent;
        private Transform target;
        private LivingEntity livingEntity;
        private bool targetIsSpotted = false;
        private bool canFire = false;
        private float shotTime;
        #endregion

        #region Private serializeField variables

        [SerializeField]
        private float speed = 3f;
        [SerializeField]
        private float minDistanceToTarget = 5f;

        [SerializeField]
        private GameObject gun;
        

        #endregion

        #region Public variables
        public static float secondsTillNextShot = 2.0f;
        public WaitForSeconds secondsTillNextShot_WaitForSeconds = new WaitForSeconds(secondsTillNextShot);

        public float nextShotTime = 2.0f;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        private void Start()
        {
            livingEntity = this.gameObject.GetComponent<LivingEntity>();
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // StartCoroutine(UpdatePath());
            // agent.SetDestination(target.position);
            StartCoroutine(ShootingTime());
        }

        // Update is called once per frame
        private void Update()
        {
            if(livingEntity.isDead == false)
            {
                if(target != null)
                {
                    this.gameObject.transform.LookAt(target.position); 
                }
            }
            StartCoroutine(UpdatePath());
            if(Time.time >= nextShotTime)
            {
                nextShotTime += Time.time;
            }
            // update the path of the agent every 0.25 seconds
            shotTime++;
            // make a ray that goes from the enemy agent to the target (player)
            Vector3 lineOrigin = this.gameObject.transform.position;
            Vector3 rayOrigin = this.gameObject.transform.position;
            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, this.gameObject.transform.forward, out hit, 1000f))
            {
                LivingEntity LivingEntity = hit.collider.GetComponent<LivingEntity>();
                if(hit.rigidbody != null)
                {
                    if(hit.rigidbody.tag == "Player")
                    {
                        // Debug.Log("Player spotted");
                        // agent.enabled = true;
                        targetIsSpotted = true;
                        // Shoot();
                        if(livingEntity.isDead == false)
                        {
                            Gun g = gun.GetComponent<Gun>();
                            // if(Time.time >= nextShotTime)
                            // {
                                // g.nextShotTime += Time.time;
                                // if(targetIsSpotted == true && Time.time >= nextShotTime)
                                // {
                                    g.Shoot();
                                    // Debug.Log("Enemy has shot at player!");
                                // }
                            // }
                        }
                        // float dist = Vector3.Distance(target.transform.position, this.gameObject.transform.position);
                        // if(dist >= minDistanceToTarget)
                        // {
                            float step = speed * Time.deltaTime; // calculate the distance to move
                            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                        // }
                    }
                    
                }
            }
        }
        IEnumerator UpdatePath()

        {
            if(livingEntity.isDead == false)
            {
                if(agent != null && target != null)
                {
                    agent.SetDestination(target.position);
                }
            }
            else if(livingEntity.isDead == true)
            {
                agent.isStopped = true;
            }
            yield return new WaitForSeconds(0.25f);
        }

        private void Shoot()
        {
            
        }
        
        IEnumerator ShootingTime()
        {
            canFire = false;
            // Debug.Log("Enemy CANNOT fire at time " + Time.time);
            yield return new WaitForSeconds(2);
            // Debug.Log("Enemy CAN fire at time " + Time.time);
            // shotTime = 0;
            canFire = true;
            /* 
            if(target != null)
            {
                gun.GetComponent<Gun>().Shoot();
            }
            */
        }

        #endregion

        #region Custom methods

        #endregion
    }
}
