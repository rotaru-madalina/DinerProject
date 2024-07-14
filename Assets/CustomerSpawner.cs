using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public Customer customer;
    public float spawnInterval = 2f;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            if(!FindObjectOfType<Entrance>().IsFull)
            {
                Instantiate(customer, spawnPoint.transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
