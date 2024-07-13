using System;
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
    private bool reachedDestination = false;
    private const float epsilon = 0.01f;
    private bool IsCloseToPlayer =>
        Physics2D.OverlapCircle(transform.position + Vector3.down, personalSpace, playerMask);

    private void Start()
    {
        CurrentMission.Start();
        CurrentMission.OnAdvanceStatusChanged += OnCurrentMissionAdvanceChange;//abonare
        Init();
    }

    private void Init()
    {
        currentDestination = transform.position;
        animator = GetComponentInChildren<SpriteAnimator>();
    }

    public void GoToPoint(Vector2 destination, Action onDone)
    {
        StartCoroutine(GoToPointRoutine(destination, onDone));
    }
    IEnumerator GoToPointRoutine(Vector2 destination, Action onDone)
    {
        currentDestination = destination;
        while(Vector2.Distance(currentDestination, destination) > epsilon)
        {
            if (IsCloseToPlayer)
            {
                animator.PlayAnimation("Idle");
                yield return null; // asteapta un frame
                continue;
            }
            //GoToPoint(GameObject.Find("Player").transform.position);

            var oldPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, currentDestination, speed * Time.deltaTime);
            var newPos = transform.position;

            if (newPos.x > oldPos.x)
                animator.PlayAnimation("WalkRight");
            else if (newPos.x < oldPos.x)
                animator.PlayAnimation("WalkLeft");

            yield return null;
        }
        animator.PlayAnimation("Idle");// stay in idle
        onDone.Invoke();
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
