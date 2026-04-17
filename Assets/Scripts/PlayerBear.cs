using UnityEngine;

public class PlayerBear : MonoBehaviour
{
    public float moveSpeed = 5f;
    [HideInInspector] public float defaultMoveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;

    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defaultMoveSpeed = moveSpeed; // Save Original Speed
    }

    void Update()
    {
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0);

        // WASD
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Face left/right
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("Speed", movement.magnitude);
    }

    void FixedUpdate()
    {
        // Move The Player
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}