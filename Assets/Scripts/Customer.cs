using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<Mission> missions;
    private List<Mission> missionsCopy = new List<Mission>();
    public LayerMask playerMask;
    public float personalSpace = 0.7f;
    public float speed = 2f;
    public GameObject attentionIndicator;
    public List<Color> colorList;

    private int currentMissionIdx = 0;
    private SpriteAnimator animator;
    private SpriteRenderer bodySprite;
    private Mission CurrentMission => missionsCopy[currentMissionIdx]; // getter
    private bool canAdvance = false;
    private bool reachedDestination = false;
    private const float epsilon = 0.01f;
    private bool IsCloseToPlayer =>
        Physics2D.OverlapCircle(transform.position + Vector3.down, personalSpace, playerMask);

    private void Start()
    {
        
        Init();
    }

    private void Init()
    {
        animator = GetComponentInChildren<SpriteAnimator>();
        bodySprite = GetComponentInChildren<SpriteRenderer>();
        bodySprite.color = colorList[
            UnityEngine.Random.Range(0, colorList.Count)];
  
        foreach (var mission in missions)
        {
            var newMission = Instantiate(mission);
            missionsCopy.Add(newMission);
            newMission.customer = this;
        }
        CurrentMission.OnAdvanceStatusChanged += OnCurrentMissionAdvanceChange;//abonare
        CurrentMission.Start();
    }

    public void GoToPoint(Vector2 destination, Action onDone = null)
    {
        StopAllCoroutines();
        StartCoroutine(GoToPointRoutine(destination, onDone));
    }
    IEnumerator GoToPointRoutine(Vector2 destination, Action onDone)
    {
        while(Vector2.Distance(transform.position, destination) > epsilon)
        {
            /*
            if (IsCloseToPlayer)
            {
                animator.PlayAnimation("Idle");
                yield return null; // asteapta un frame
                continue;
            }
            */
            //GoToPoint(GameObject.Find("Player").transform.position);

            var oldPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            var newPos = transform.position;

            if (newPos.x > oldPos.x)
                animator.PlayAnimation("WalkRight");
            else if (newPos.x < oldPos.x)
                animator.PlayAnimation("WalkLeft");

            yield return null;
        }
        animator.PlayAnimation("Idle");// stay in idle
        onDone?.Invoke();
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
        if(attentionIndicator != null)
            attentionIndicator.SetActive(canAdvance); // alertam playerul
        this.canAdvance = canAdvance;
    }
}
