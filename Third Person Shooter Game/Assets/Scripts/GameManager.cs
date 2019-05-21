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
        private Transform [] enemySpawnPoints;
        private Transform [] healthPowerUpsSpawnPoints;

        // variables in charge of handling time
        private float auxTime;
        private int auxIndex = 0;

        #endregion

        #region Private serializeField variables

        [SerializeField]
        private float msBetweenEnemyWaves = 3f;
        [SerializeField]
        private float msBetweenHealthPowerUpWaves = 3f;
        [SerializeField]
        private GameObject enemy;
        [SerializeField]
        private GameObject healthPowerUp;
        [SerializeField]
        private float [] levelTimes;

        #endregion

        #region Public variables

        public GameObject enemySpawnPointsGameObject;
        public GameObject healthPowerUpsSpawnPointsGameObject;
        

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            // call the spawn function after a delay of msBetweenWaves, and then continuw to call after the same amount of time
            InvokeRepeating("SpawnEnemies", msBetweenEnemyWaves, msBetweenEnemyWaves);
            player = GameObject.FindGameObjectWithTag("Player");
            if((enemySpawnPointsGameObject.transform.childCount > 0) != true)
            {
                Debug.LogError("There are no spawning objects assigned to the spawning points GameObject");
            }
            enemySpawnPoints = new Transform[enemySpawnPointsGameObject.transform.childCount];
            for(int i = 0; i < enemySpawnPointsGameObject.transform.childCount; i++)
            {
                enemySpawnPoints[i] = enemySpawnPointsGameObject.transform.GetChild(i);
            }

            // call the spawning health power ups
            InvokeRepeating("SpawnHealthPowerUps", msBetweenHealthPowerUpWaves, msBetweenHealthPowerUpWaves);
            player = GameObject.FindGameObjectWithTag("Player");
            if((healthPowerUpsSpawnPointsGameObject.transform.childCount > 0) != true)
            {
                Debug.LogError("There are no health power up spawning objects assigned to the health power up spawning points GameObject");
            }
            healthPowerUpsSpawnPoints = new Transform[healthPowerUpsSpawnPointsGameObject.transform.childCount];
            for(int i = 0; i < healthPowerUpsSpawnPointsGameObject.transform.childCount; i++)
            {
                healthPowerUpsSpawnPoints[i] = healthPowerUpsSpawnPointsGameObject.transform.GetChild(i);
            }
        }

        private void Update()
        {
            auxTime = Time.time;
            auxIndex = 0;
            if(auxTime > levelTimes[auxIndex] %% msBetweenEnemyWaves >= 1 )
            {
                msBetweenEnemyWaves--;
            }

        }


        #endregion
        private void SpawnEnemies()
        {
            if(player != null)
            {
                if(player.GetComponent<LivingEntity>().isDead == true)
                {
                    return;
                }
            }

            // Find a random value (for the index) between zero and the lenght of the spawnPoints array
            int spawnpointIndex = Random.Range(0, enemySpawnPoints.Length);
            // create an instance of the enemy at the random spawn point
            Instantiate(enemy, enemySpawnPoints[spawnpointIndex].position, enemySpawnPoints[spawnpointIndex].rotation);
        }
        private void SpawnHealthPowerUps()
        {
            // Find a random value (for the index) between zero and the lenght of the spawnPoints array
            int spawnpointIndex = Random.Range(0, enemySpawnPoints.Length);
            // create an instance of the health power up at the random spawn point
            Instantiate(healthPowerUp, healthPowerUpsSpawnPoints[spawnpointIndex].position, healthPowerUpsSpawnPoints[spawnpointIndex].rotation);
        }

        #region Custom methods

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}
