using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GoToTableAndWaitMission")]

public class GoToTableAndWaitMission : Mission
{
    public Vector2 table1Pos;
    public float waitDuration;
    public override void Advance()
    {
        customer.GoToPoint(new Vector2(100, 0));
    }

    public override void Start()
    {
        customer.GoToPoint(table1Pos, () =>
        {
            customer.animator.PlayAnimation("Sit");
            customer.DelayedAction(waitDuration, () => OnAdvanceStatusChanged?.Invoke(true));
        });
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {

    }
}
