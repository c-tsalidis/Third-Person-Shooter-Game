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
        #endregion

        #region Private serializeField variables

        [SerializeField]
        private float speed = 3f;
        [SerializeField]
        private float minDistanceToTarget = 5f;

        #endregion

        #region Public variables

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        private void Start()
        {
            livingEntity = this.gameObject.GetComponent<LivingEntity>();
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            // agent.SetDestination(target.position);
            // StartCoroutine(UpdatePath());
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
            // update the path of the agent every 0.25 seconds
            StartCoroutine(UpdatePath());
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
                agent.SetDestination(target.position);
            }
            else if(livingEntity.isDead == true)
            {
                agent.isStopped = true;
            }
            yield return new WaitForSeconds(0.25f);
        }

        #endregion

        #region Custom methods

        #endregion
    }
}
