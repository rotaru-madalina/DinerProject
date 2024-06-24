using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteOrderManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sprites = FindObjectsOfType<OrderedSprite>().ToList();
        sprites.Sort(CompareByYPosition);//lista descresc.

        for(int i = 0; i < sprites.Count; i++)
        {
            sprites[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }
    }

    int CompareByYPosition(OrderedSprite a, OrderedSprite b)
    {
        if (a.transform.position.y < b.transform.position.y)
            return 1;
        else if (a.transform.position.y > b.transform.position.y)
            return -1;
        else
            return 0;
    }

}
