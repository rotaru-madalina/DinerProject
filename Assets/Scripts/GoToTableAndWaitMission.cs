using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GoToTableAndWaitMission")]

public class GoToTableAndWaitMission : Mission
{
    public float waitDuration;
    Table masaLaCareTreSaMerg;
    public override void Advance()
    {
        FindObjectOfType<TableManager>().FreeTable(masaLaCareTreSaMerg);
        customer.GoToPoint(new Vector2(100, 0));
    }

    public override void Start()
    {
        masaLaCareTreSaMerg = FindObjectOfType<TableManager>().GetFreeTable();

        if (masaLaCareTreSaMerg == null)
            return;

        masaLaCareTreSaMerg.isOccupied = true;
        Vector2 tablePos = masaLaCareTreSaMerg.sittingPoint.transform.position;
        customer.GoToPoint(tablePos, () =>
        {
            customer.animator.PlayAnimation("Sit");
            customer.DelayedAction(waitDuration, () =>
            {
                OnAdvanceStatusChanged?.Invoke(true);
            });
        });
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {

    }
}
