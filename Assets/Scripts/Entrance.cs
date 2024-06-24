using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public float distance = 2f;

    private int customersLength;
    // Start is called before the first frame update
    private void addInQueue()
    {
        customersLength++;
    }
    private void extractFromQueue()
    {
        customersLength--;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
