using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AstralPlayerController : MonoBehaviour
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

    private Vector3 right;
    private Vector3 forward;

    Animator animator;

    private void Awake()
    {
        playerInput = new JoystickController();
        controller = GetComponent<CharacterController>();
        //finding active camera
        activeCamera = (Camera)FindObjectOfType(typeof(Camera));
        animator = transform.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        //finding active camera runtime
        activeCamera = (Camera)FindObjectOfType(typeof(Camera));
    }

    private void Move()
    {
        if (gamePause.isFrozen == false)//makes animations uninterruptable
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
                gameObject.transform.forward = move;
                animator.speed = 2;
            }
            else
            {
                animator.speed = 0.5f;
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

