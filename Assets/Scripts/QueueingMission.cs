using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewQueueingMission")]

public class QueueingMission : Mission
{
    public override void Advance()
    {
        //customer.GoToPoint(new Vector2(100, 0));
        FindObjectOfType<Entrance>().Remove();
    }

    public override void Start()
    {
        FindObjectOfType<Entrance>().Add(customer, () =>
        {
            if (FindObjectOfType<TableManager>().GetFreeTable() == null)
                FindObjectOfType<TableManager>().OnFreeTableAvailable += () => OnAdvanceStatusChanged?.Invoke(true);
            else
                OnAdvanceStatusChanged?.Invoke(true);
        });
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {

    }
}
