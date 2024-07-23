using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public float distance = 1f;
    public int capacity = 4;
    public bool IsFull => NumberOfCustomers >= capacity;

    private List<Customer> customerList = new List<Customer>();
    private int NumberOfCustomers => customerList.Count;
    private Vector2 LastSlot => FirstSlot + Vector2.left * NumberOfCustomers * distance;
    private Dictionary<Customer, Action> customersDict = new Dictionary<Customer, Action>();

    private Vector2 FirstSlot => (Vector2)transform.position;

    public Vector2 GetPositionOfIndex(int index)
    {
        return FirstSlot + Vector2.left * index * distance;
    }
    public void Add(Customer newCustomer, Action onReachFirst = null)
    {
        if (IsFull) return;
        if (customersDict.ContainsKey(newCustomer)) return;
        customersDict.Add(newCustomer, onReachFirst);
        if (NumberOfCustomers == 0)
        {
            newCustomer.GoToPoint(LastSlot, onReachFirst);
        }
        else
            newCustomer.GoToPoint(LastSlot);

        customerList.Add(newCustomer);
    }
    public void Remove()
    {
        for (int i = 1; i < customerList.Count; i++)
        {
            Customer customer = customerList[i];
            Action onEnd = (i == 1 ? customersDict[customer] : null);
            //var oneStepForward = (Vector2)customer.transform.position +
            //    Vector2.right * distance;
            //if (Vector2.Distance(oneStepForward, (Vector2)transform.position) < 0.2f)
            //    customer.GoToPoint(oneStepForward, customersDict[customer]);
            //else customer.GoToPoint(oneStepForward);
            customer.GoToPoint(GetPositionOfIndex(i - 1), onEnd);

        }
        customerList.RemoveAt(0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
