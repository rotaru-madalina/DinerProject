using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteAnimator animator;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<SpriteAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rigidbody.velocity = (new Vector2(horizontalInput, verticalInput).normalized) * speed;
    }

    private void Animate()
    {
        var currentSpeed = rigidbody.velocity.normalized;

        if (currentSpeed == Vector2.left)
        {
            animator.PlayAnimation("WalkLeft");
        }
        else if (currentSpeed == Vector2.right)
        {
            animator.PlayAnimation("WalkRight");
        }
        else if (currentSpeed == Vector2.down)
        {
            animator.PlayAnimation("WalkBackwards");
        }
        else if (currentSpeed == Vector2.up)
        {
            animator.PlayAnimation("WalkForward");
        }
        else if (currentSpeed == Vector2.zero)
        {
            animator.PlayAnimation("Idle");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Customer"))
        {
            collision.gameObject.GetComponentInParent<Customer>().TryAdvance();
        }
    }
}
