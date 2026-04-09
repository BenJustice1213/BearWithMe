using UnityEngine;

public class PlayerBear : MonoBehaviour
{
    public float moveSpeed = 5f;
    [HideInInspector] public float defaultMoveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultMoveSpeed = moveSpeed; // Save Original Speed
    }

    void Update()
    {
        // WASD
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Move The Player
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}