using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    public bool fillRedGlass = false;
    public bool fillGreenGlass = false;
    public bool emptyGlass = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(fillRedGlass)
            player.HasRedGlass = true;

        if(fillGreenGlass)
            player.HasGreenGlass = true;

        if(emptyGlass)
        {
            player.HasGreenGlass = false;
            player.HasRedGlass = false;
        }
    }
}
