using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewQueueingMission")]

public class QueueingMission : Mission
{
    public override void Advance()
    {

    }

    public override void Start()
    {
        OnAdvanceStatusChanged?.Invoke(true); // strigat, notificare abonati
    }

    protected override void OnEnd()
    {

    }

    protected override void OnStart()
    {

    }
}
