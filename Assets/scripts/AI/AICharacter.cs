using UnityEngine;

public class AICharacter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float roamRadius = 8f;
    [SerializeField] private float stoppingDistance = 0.2f;
    [SerializeField] private float gravity = -20f;

    [SerializeField] private float lifetime = 10f;
    [SerializeField] private Animator animator;

    private CharacterController controller;
    private Vector3 targetPosition;
    private float verticalVelocity;
    private float lifeTimer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        lifeTimer = 0f;
        verticalVelocity = 0f;

        ChooseNewTarget();

        animator.Play("Walking");
    }

   

    private void Update()
    {
        // Lifetime timer
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifetime)
        {
            gameObject.SetActive(false);
            return;
        }

        // Gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        // Find direction
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        // Reached destination
        if (direction.magnitude <= stoppingDistance)
        {
            ChooseNewTarget();
            return;
        }

        direction.Normalize();

        // Face movement direction
        transform.rotation = Quaternion.LookRotation(direction);

        // Move
        Vector3 movement = direction * moveSpeed;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);
    }

    private void ChooseNewTarget()
    {
        Vector2 randomPoint =
            Random.insideUnitCircle * roamRadius;

        targetPosition = new Vector3(
            transform.position.x + randomPoint.x,
            transform.position.y,
            transform.position.z + randomPoint.y
        );
    }
}