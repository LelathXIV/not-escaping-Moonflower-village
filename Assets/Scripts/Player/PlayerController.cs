using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private JoystickController playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [Inject] private IGamePauseService gamePause;
    private Animator playerAnimator;
    public Camera activeCamera;
    public PlayerShootingSystem playerShootingSystem;
    public BattleMode battleMode;

    private Vector3 right;
    private Vector3 forward;

    private void Awake()
    {
        playerInput = new JoystickController();
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();

        //finding active camera
        activeCamera = (Camera)FindObjectOfType(typeof(Camera));
        playerShootingSystem = GetComponent<PlayerShootingSystem>();
        battleMode = GetComponent<BattleMode>();
    }

    void Update()
    {
        Move();

        //finding active camera runtime
        activeCamera = (Camera)FindObjectOfType(typeof(Camera));
    }

    private void Move()
    {
        if (gamePause.isFrozen == false)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
   
            Vector2 movementInput = playerInput.JSController.Move.ReadValue<Vector2>();
            Vector3 move = Vector3.zero;

            if (movementInput == Vector2.zero)
            {
                right = Vector3.Cross(Vector3.up, activeCamera.transform.forward);
                forward = Vector3.Cross(right, Vector3.up);
            }

            move = (right * movementInput.x) + (forward * movementInput.y);

            //camera recalculations - correcting axises


            if (move != Vector3.zero)
            {
                //walking happens here!!
                playerShootingSystem.StopAiming();
                gameObject.transform.forward = move;
                Walk();
            }
            else if(move == Vector3.zero)
            {
                Idle();
                if (battleMode.isInBattle == true)
                {
                    battleMode.LookAtTargetEnemy();
                }
            }

            controller.Move(move * Time.deltaTime * playerSpeed);
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
     
    //animation
    private void Idle()
    {
        playerAnimator.SetFloat("speed", 0);
    }
    //animation
    private void Walk()
    {
        playerAnimator.SetFloat("speed", 0.5f);
    }
    //animation
    private void Run()
    {
        playerAnimator.SetFloat("speed", 1);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

}
