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
        private float levelUpTime;
        [SerializeField]
        private GameObject pausedMenuPanel;

        #endregion

        #region Public variables

        public bool gamePaused;
        public GameObject enemySpawnPointsGameObject;
        public GameObject healthPowerUpsSpawnPointsGameObject;
        public int level = 1;
        public int enemyCounter = 0;
        public float auxTime = 0;

        [Space]
        public bool isFirstPerson = true;
        public bool isThirdPerson = false;
        

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {

            pausedMenuPanel.SetActive(false);

            // increase the level over time
            // InvokeRepeating("LevelUp", levelUpTime, levelUpTime);

            player = GameObject.FindGameObjectWithTag("Player");

            // call the spawn function after a delay of msBetweenWaves, and then continuw to call after the same amount of time
            InvokeRepeating("SpawnEnemies", msBetweenEnemyWaves, msBetweenEnemyWaves);
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
            // player = GameObject.FindGameObjectWithTag("Player");
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
            // auxTime = Time.time;
            // if(auxTime > (1 +(Time.time - auxTime)) && msBetweenEnemyWaves >= 0.25f )
            // {
                // msBetweenEnemyWaves -= 0.25f;
            //     auxTime = 0;
                // level = Mathf.RoundToInt(Time.time / 1000);;
            // }

            if(player == null)
            {
                return;
            }

            enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if((player.GetComponent<Player>().score % 10) == 0 && msBetweenEnemyWaves >= 0.5f)
            {
                // LevelUp();
                msBetweenEnemyWaves -= 0.25f;
                level++;
            }

            // checking if the player wants to pause the game
            if(Input.GetButtonDown("Cancel"))
            {
                if(gamePaused == false)
                {
                    Time.timeScale = 0;
                    gamePaused = true;
                    pausedMenuPanel.SetActive(true);
                }
            }

            // checking id the player is switching from first person to third person and viceversa
            if (Input.GetMouseButtonDown(1))
            {
                if(isFirstPerson == true)
                {
                    isFirstPerson = false;
                    isThirdPerson = true;
                }
                else if(isThirdPerson == true)
                {
                    isFirstPerson = true;
                    isThirdPerson = false;
                }
                else
                {
                    // by default it's the first person
                    isFirstPerson = true;
                }
            }

        }


        #endregion


        #region Custom methods
        
        private void LevelUp()
        {
            level++;
            msBetweenEnemyWaves -= 1000f;
        }
        
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


        public void ContinuePlaying()
        {
            pausedMenuPanel.SetActive(false);
            Time.timeScale = 1;
            gamePaused = false;
        }


        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}
