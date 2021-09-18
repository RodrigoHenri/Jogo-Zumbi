using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public bool CanPlay = true;

    [Header("Movement Variables")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Shooting Variables")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform SpawnShoots;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletHitMissDistance = 25f;
    [SerializeField] private float DelaySpawnShoot = 0.3f;

    [Header("Gun Audio Variables")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private bool CanShootAgain = true;

    [Header("Target Variables")]
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float aimDistance = 1f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    
    private PlayerInput playerInput;    
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    [Header("Character Animator Variables")]
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSmoothTime = 0.1f;
    [SerializeField] private float animationPlayTransition = 0.15f;
    int jumpAnimation;
    int recoilAnimation;
    int moveXAnimationParameterId;
    int moveZAnimationParameterId;
    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;

    [Header("Player Audio Variables")]
    [SerializeField] private AudioSource PlayerAudio;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

       
        Cursor.lockState = CursorLockMode.Locked;

        
        animator = GetComponent<Animator>();
        jumpAnimation = Animator.StringToHash("Pistol Jump");
        recoilAnimation = Animator.StringToHash("PistolShootRecoil");
        moveXAnimationParameterId = Animator.StringToHash("MoveX");
        moveZAnimationParameterId = Animator.StringToHash("MoveZ");
        
    }

    private void OnEnable()
    {
       shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun()
    {
        if (CanShootAgain == true && Time.timeScale == 1f && CanPlay == true)
        {
            audio.Pause();
            StartCoroutine(YouCanShootNow());
            audio.Play(0);
            RaycastHit hit;
            GameObject bullet = GameObject.Instantiate(bulletPrefab, SpawnShoots.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();


            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;
            }
            else
            {
                bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
                bulletController.hit = false;
            }
            animator.CrossFade(recoilAnimation, animationPlayTransition);

        }

    }


    void Update()
    {
        if (Time.timeScale == 1f && CanPlay == true)
        {


            aimTarget.position = cameraTransform.position + cameraTransform.forward * aimDistance;

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = moveAction.ReadValue<Vector2>();
            currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);
            Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);

            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;

            controller.Move(move * Time.deltaTime * playerSpeed);
            animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
            animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);


           
            controller.Move(playerVelocity * Time.deltaTime);


           
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public IEnumerator YouCanShootNow()
    {
        CanShootAgain = false;
        yield return new WaitForSeconds(DelaySpawnShoot);
        CanShootAgain = true;
    }
}
