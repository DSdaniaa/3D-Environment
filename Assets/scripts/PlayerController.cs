using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -20f;

    [SerializeField] private Animator animator;

    public Animator Animator => animator;

    private CharacterController controller;
    private float verticalVelocity;

    // State Machine
    private PlayerStateMachine stateMachine;

    // States
    private IdleState idleState;
    private WalkState walkState;
    private RunState runState;
    private JumpState jumpState;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        runState = new RunState(this);
        jumpState = new JumpState(this);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        // -------------------------
        // INPUT
        // -------------------------

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (joystick != null && joystick.Input.magnitude > 0.1f)
        {
            horizontal = joystick.Input.x;
            vertical = joystick.Input.y;
        }

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement =
            cameraForward * vertical +
            cameraRight * horizontal;

        bool isMoving = movement.magnitude > 0.1f;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }
        bool isRunning = Input.GetKey(KeyCode.LeftShift);


        // -------------------------
        // GROUND CHECK
        // -------------------------

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }


        // -------------------------
        // STATE MACHINE
        // -------------------------

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.ChangeState(jumpState);
            }
            else if (!isMoving)
            {
                stateMachine.ChangeState(idleState);
            }
            else if (isRunning)
            {
                stateMachine.ChangeState(runState);
            }
            else
            {
                stateMachine.ChangeState(walkState);
            }
        }


        // -------------------------
        // SPEED
        // -------------------------

        float currentSpeed;

        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        movement *= currentSpeed;


        // -------------------------
        // JUMP
        // -------------------------

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }


        // -------------------------
        // GRAVITY
        // -------------------------

        verticalVelocity += gravity * Time.deltaTime;

        movement.y = verticalVelocity;


        // -------------------------
        // MOVE
        // -------------------------

        controller.Move(movement * Time.deltaTime);

        // Update current state
        stateMachine.Update();
    }
}