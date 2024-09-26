using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public List<Table> tables;
    public event Action OnFreeTableAvailable;

    public Table GetFreeTable()
    {
        foreach (Table table in tables)
        {
            if(!table.isOccupied)
                return table;
        }
        return null;
    }

    public void FreeTable(Table table)
    {
        table.isOccupied = false;
        OnFreeTableAvailable.Invoke();
    }
}
