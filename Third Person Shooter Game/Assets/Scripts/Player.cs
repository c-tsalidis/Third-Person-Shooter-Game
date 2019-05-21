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
        private Vector3 cameraPositionOffset;
        [SerializeField]
        private Quaternion cameraRotationOffset;

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
            MovePlayer();
            CameraFollow();
            CheckGuns();
            Shoot();
        }

        private void FixedUpdate()
        {

        }

        #endregion

        #region Custom methods


        private void MovePlayer()
        {
            float horizontal = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
            float vertical = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;
            isJumping = Input.GetKeyDown(KeyCode.Space);
            Vector3 moveInput = new Vector3(horizontal, 0, vertical);
            float mouseInputXAxis = Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime;
            // float mouseInputYAxis = Input.GetAxis("Mouse Y") * rotateSpeed / 2 * Time.fixedDeltaTime;
            // Vector3 rotationVector = new Vector3(-mouseInputYAxis, mouseInputXAxis, 0);
            Vector3 rotationVector = new Vector3(0, mouseInputXAxis, 0);
            /*
            if(moveInput != new Vector3(0,0,0))
            {
                rotationVector = Vector3.Lerp(rotationVector, new Vector3(0, mouseInputXAxis, 0), Time.time);
            }
             */

            gameObject.transform.Rotate(rotationVector);
            gameObject.transform.Translate(horizontal * moveSpeed, 0, vertical * moveSpeed);

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
            camera.transform.position = this.gameObject.transform.position + cameraPositionOffset;
            camera.transform.rotation = this.gameObject.transform.rotation;
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
            Vector3 uiPos = Camera.main.WorldToScreenPoint(hitPoint);
            GameObject newTakeDamagePanel = Instantiate(takeDamagePanel, takeDamagePanel.transform.position, Quaternion.identity); 
        }

        public void IncreaseHealth(float addedHealth)
        {
            health += addedHealth;
        }

        #endregion
    }
}
