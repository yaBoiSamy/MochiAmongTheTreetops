using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolnfTerritory : MonoBehaviour
{
    [HideInInspector]
    public bool territoryEntered;

    void start()
    {
        territoryEntered = false;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            territoryEntered = true;
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            territoryEntered = false;
        }
    }
}
