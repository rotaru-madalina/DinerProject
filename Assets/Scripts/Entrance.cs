using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public float distance = 1f;
    public int capacity = 4;
    public bool IsFull => Length >= capacity;

    private List<Customer> customerList = new List<Customer>();
    private int Length => customerList.Count;
    private Vector2 LastSlot => (Vector2)transform.position + 
        Vector2.left * Length * distance;
    private Dictionary<Customer, Action> customersDict = new Dictionary<Customer, Action>();


    // Start is called before the first frame update
    public void Add(Customer newCustomer, Action onReachFirst = null)
    {
        if (IsFull) return;
        if (customersDict.ContainsKey(newCustomer)) return;
        customersDict.Add(newCustomer, onReachFirst);
        if (Length == 0)
        {
            newCustomer.GoToPoint(LastSlot, onReachFirst);
        }
        else
            newCustomer.GoToPoint(LastSlot);

        customerList.Add(newCustomer);
    }
    public void Remove()
    {
        customerList.RemoveAt(0);
        foreach (var customer in customerList)
        {
            var oneStepForward = (Vector2)customer.transform.position +
                Vector2.right * distance;
            if (Vector2.Distance(oneStepForward, (Vector2)transform.position) < 0.2f)
                customer.GoToPoint(oneStepForward, customersDict[customer]);
            else customer.GoToPoint(oneStepForward);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
