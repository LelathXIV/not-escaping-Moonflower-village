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
    public Camera activeCamera;
    public PlayerShootingSystem playerShootingSystem;
    public BattleMode battleMode;
    public PlayerAnimations playerAnimations;

    private Vector3 right;
    private Vector3 forward;

    private void Awake()
    {
        playerInput = new JoystickController();
        controller = GetComponent<CharacterController>();
        playerAnimations = GetComponent<PlayerAnimations>();
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
        if (gamePause.isFrozen == false && !playerAnimations.isInteracting)//makes animations uninterruptable
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
            //camera recalculations - correcting axises
            move = (right * movementInput.x) + (forward * movementInput.y);

            if (move != Vector3.zero )
            {
                //walking happens here!!
                playerShootingSystem.StopAiming();
                gameObject.transform.forward = move;
                if(battleMode.isInBattle) {playerAnimations.Run(); }
                if(!battleMode.isInBattle) { playerAnimations.Walk(); }
            }
            else if(move == Vector3.zero)
            {
                playerAnimations.Idle();

                if (battleMode.isInBattle == true)
                {
                    battleMode.LookAtTargetEnemy();
                }
                if (playerShootingSystem.isAiming && !playerShootingSystem.isReloading)
                {
                    playerAnimations.Aiming();
                }
            }
            controller.Move(move * Time.deltaTime * playerSpeed);
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
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
