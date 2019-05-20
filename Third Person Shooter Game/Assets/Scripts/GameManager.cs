using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    public class GameManager : MonoBehaviour
    {
        #region Private non-serializefield variables
        
        private GameObject player;
        private Transform [] spawnPoints;

        #endregion

        #region Private serializeField variables

        [SerializeField]
        private float msBetweenWaves = 3f;
        [SerializeField]
        private GameObject enemy;

        #endregion

        #region Public variables

        public GameObject spawnPointsGameObject;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            // call the spawn function after a delay of msBetweenWaves, and then continuw to call after the same amount of time
            InvokeRepeating("Spawn", msBetweenWaves, msBetweenWaves);
            player = GameObject.FindGameObjectWithTag("Player");
            if((spawnPointsGameObject.transform.childCount > 0) != true)
            {
                Debug.LogError("There are no spawning objects assigned to the spawning points GameObject");
            }
            spawnPoints = new Transform[spawnPointsGameObject.transform.childCount];
            for(int i = 0; i < spawnPointsGameObject.transform.childCount; i++)
            {
                spawnPoints[i] = spawnPointsGameObject.transform.GetChild(i);
            }
        }

        private void Spawn()
        {
            if(player != null)
            {
                if(player.GetComponent<LivingEntity>().isDead == true)
                {
                    return;
                }
            }

            // Find a random value (for the index) between zero and the lenght of the spawnPoints array
            int spawnpointIndex = Random.Range(0, spawnPoints.Length);
            // create an instance of the enemy at the random spawn point
            Instantiate(enemy, spawnPoints[spawnpointIndex].position, spawnPoints[spawnpointIndex].rotation);
        }

        #endregion

        #region Custom methods

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}
