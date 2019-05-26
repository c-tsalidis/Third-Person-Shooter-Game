using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.ctsalidis.ThirdPersonShooterGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {

        #region Private non-serializefield variables
        
        
        private Rigidbody rb;
        private bool isJumping;
        private bool canJump = true; // to check whether or not the player can jump (if the player is already jumping, don't let him jump more)
        private bool isDead;
        private Camera camera;
        private GameObject equipedGun;
        private GameManager gameManager;


        #endregion

        #region Private serializeField variables
        
        [Header("Player movement")]
        [SerializeField]
        private float moveSpeed = 10f;
        
        [SerializeField]
        private float rotateSpeed = 5f;
        
        [SerializeField]
        private float jumpForce = 5f;

        [Header("Camera")]
        [SerializeField]
        private GameObject cameraTarget;
        [SerializeField]
        private Vector3 firstPersonCameraPositionOffset;
        [SerializeField]
        private Vector3 thirdPersonCameraPositionOffset = new Vector3(0, 20, -15);
        [SerializeField]
        private Quaternion firstPersonCameraRotationOffset;
        [SerializeField]
        private Camera firstPersonCamera;
        [SerializeField]
        private Camera thirdPersonCamera;

        [SerializeField]
        private GameObject [] guns;

        [Space]
        [SerializeField]
        private GameObject takeDamagePanel;

        
        #endregion

        #region Public variables

        public static Player Instance; // singleton
        public float score; // the score of the player is the time he stays alive
        public float health;

        #endregion

        #region Monobehabiour callback methods

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            rb = this.gameObject.GetComponent<Rigidbody>();
            camera = Camera.main;

            // by default only the first gun in the array (index 0) is equiped
            for(int i = 0; i < guns.Length; i++)
            {
                Gun gun = guns[i].GetComponent<Gun>();
                if(gun != null)
                {
                    if(i == 0)
                    {
                        gun.isEquiped = true;
                    }
                    else
                    {
                        gun.isEquiped = false;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            health = this.gameObject.GetComponent<LivingEntity>().health;
            if(health > 0f)
            {
                // score = Mathf.Floor(Time.time * 10);
            }

            float highScore = PlayerPrefs.GetFloat("SurvivalMode_Player_HighScore");
            if(score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetFloat("SurvivalMode_Player_HighScore", highScore);
            }

            if(gameManager.gamePaused == false)
            {
                MovePlayer();
                CameraFollow();
                CheckGuns();
                Shoot();
            }
        }

        private void FixedUpdate()
        {

        }

        #endregion

        #region Custom methods


        private void MovePlayer()
        {
            float horizontal = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * moveSpeed;
            float vertical = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * moveSpeed;
            isJumping = Input.GetKeyDown(KeyCode.Space);
            Vector3 moveInput = new Vector3(horizontal, 0, vertical);

            // if it's first person shooter
            if(gameManager.isFirstPerson)
            {
                float mouseInputXAxis = Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime;
                // float mouseInputYAxis = Input.GetAxis("Mouse Y") * rotateSpeed / 2 * Time.fixedDeltaTime;
                // Vector3 rotationVector = new Vector3(-mouseInputYAxis, mouseInputXAxis, 0);
                Vector3 rotationVector = new Vector3(0, mouseInputXAxis, 0);
                gameObject.transform.Rotate(rotationVector);
                gameObject.transform.Translate(moveInput);
            }
            // if it's third person shooter
            else if(gameManager.isThirdPerson)
            {

                Ray ray = thirdPersonCamera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;

                if(groundPlane.Raycast(ray, out rayDistance))
                {
                    Vector3 point = ray.GetPoint(rayDistance);
                    Debug.DrawLine(ray.origin, point, Color.red);
                    Vector3 heightCorrectionLookPoint = new Vector3(point.x, transform.position.y, point.z);
                    transform.LookAt(heightCorrectionLookPoint);
                    // gun.transform.LookAt(heightCorrectionLookPoint);
                }

                // move with rigidbody, not with translate because translate will move and rotate relative to the player's rotation
                rb.MovePosition(rb.position + moveInput ); 
            }


            if(isJumping == true)
            {
                if(canJump == true)
                {
                    StartCoroutine(Jump());
                }
            }
        }

        IEnumerator Jump()
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
            canJump = false;
            yield return new WaitForSeconds(1);
            canJump = true;
        }

        private void CameraFollow()
        {
            if(gameManager.isFirstPerson == true)
            {
                firstPersonCamera.enabled = true;
                thirdPersonCamera.enabled = false;
                camera.transform.position = this.gameObject.transform.position + firstPersonCameraPositionOffset;
                camera.transform.rotation = this.gameObject.transform.rotation;
            }
            else if(gameManager.isThirdPerson == true)
            {

                firstPersonCamera.enabled = false;
                thirdPersonCamera.enabled = true;
                thirdPersonCamera.transform.position = this.gameObject.transform.position + thirdPersonCameraPositionOffset;
            }
            else
            {
                // if none of the cameras are enabled, then set the first person shooter as enabled by default
                gameManager.isFirstPerson = true;
            }
        }


        private void CheckGuns()
        {
            for(int i = 0; i < guns.Length; i++)
            {
                if(guns[i].GetComponent<Gun>().isEquiped == true)
                {
                    equipedGun = guns[i];
                }
            }
        }

        private void Shoot()
        {
            if(gameObject != null)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    equipedGun.GetComponent<Gun>().Shoot();
                }
            }
        }

        public void ShowTakingDamagePanel(Vector3 hitPoint)
        {
            if(takeDamagePanel == null)
            {
                Debug.LogError("Player doesn't have the takeDamagePanel gameobject attached to it");
                return;
            }
            // Vector3 uiPos = Camera.main.WorldToScreenPoint(hitPoint);
            // GameObject newTakeDamagePanel = Instantiate(takeDamagePanel, takeDamagePanel.transform.position, Quaternion.identity); 
        }

        #endregion
    }
}
