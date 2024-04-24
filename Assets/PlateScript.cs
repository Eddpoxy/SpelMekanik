using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    int itemCount = 0;
    public GameObject Door;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Increment counter if the entering object's name contains "Item"
        if (other.gameObject.name.Contains("Item"))
        {
            itemCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Decrement counter if the exiting object's name contains "Item"
        if (other.gameObject.name.Contains("Item"))
        {
            itemCount--;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if there are exactly twenty items inside
        if (itemCount == 10)
        {
            Destroy(Door);
        }
    }
}