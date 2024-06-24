using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Vector2 currentDestination;
    public float speed = 2f;
    public LayerMask playerMask;
    public float personalSpace = 0.7f;

    private SpriteAnimator animator;

    private void Start()
    {
        currentDestination = transform.position;
        animator = GetComponentInChildren<SpriteAnimator>();
    }
    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down, personalSpace, playerMask))
        {
            animator.PlayAnimation("Idle");
            return;
        }
        GoToPoint(GameObject.Find("Player").transform.position);

        var oldPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, currentDestination, speed * Time.deltaTime);
        var newPos = transform.position;

        if (newPos.x > oldPos.x)
            animator.PlayAnimation("WalkRight");
        else if (newPos.x < oldPos.x)
            animator.PlayAnimation("WalkLeft");

    }
    public void GoToPoint(Vector2 destination)
    {
        currentDestination = destination;
    }

}
