using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<Mission> missions;
    public LayerMask playerMask;
    public float personalSpace = 0.7f;
    public float speed = 2f;
    public GameObject attentionIndicator;

    private int currentMissionIdx = 0;
    private SpriteAnimator animator;
    private Vector2 currentDestination;
    private Mission CurrentMission => missions[currentMissionIdx]; // getter
    private bool canAdvance = false;

    private void Start()
    {
        CurrentMission.Start();
        CurrentMission.OnAdvanceStatusChanged += OnCurrentMissionAdvanceChange;//abonare
        Init();
    }
    private void Update()
    {
        UpdateMovement();
    }

    private void Init()
    {
        currentDestination = transform.position;
        animator = GetComponentInChildren<SpriteAnimator>();
    }

    private void UpdateMovement()
    {
        if (Physics2D.OverlapCircle(transform.position + Vector3.down, personalSpace, playerMask))
        {
            animator.PlayAnimation("Idle");
            return;
        }
        //GoToPoint(GameObject.Find("Player").transform.position);

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

    public void TryAdvance()
    {
        if (canAdvance)
            Advance();
    }

    private void Advance()
    {
        CurrentMission.Advance();
        CurrentMission.OnAdvanceStatusChanged -= OnCurrentMissionAdvanceChange;//dezabonare misiune curenta
        currentMissionIdx++;
        canAdvance = false;
        attentionIndicator.SetActive(false);
        CurrentMission.Start();
        CurrentMission.OnAdvanceStatusChanged += OnCurrentMissionAdvanceChange;//abonare misiunea noua
    }

    private void OnCurrentMissionAdvanceChange(bool canAdvance)
    {
        attentionIndicator.SetActive(canAdvance); // alertam playerul
        this.canAdvance = canAdvance;
    }
}
