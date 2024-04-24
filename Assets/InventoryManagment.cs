using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagment : MonoBehaviour
{
    public List<Image> Flags;
   
    public List<int> Inventory;
    public List<GameObject> Prefabs;
    bool Pickup;
    private void OnTriggerStay(Collider other)
    {
        if (Pickup)
        {
            if (other.gameObject.name.Contains("Item"))
            {
                if (other.gameObject.name.Contains("Item1"))
                {
                    Inventory.Add(1);
                } 
                else if (other.gameObject.name.Contains("Item2"))
                {
                    Inventory.Add(2);
                } 
                else if (other.gameObject.name.Contains("Item3"))
                {
                    Inventory.Add(3);
                }
                Destroy(other.gameObject); 
               
            }
        }
       

    }
  

    // Update is called once per frame
    void Update()
    {
        Pickup = Input.GetKey(KeyCode.E);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveItemToFront(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveItemToFront(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MoveItemToFront(3);
        }  
        
        
        if (Inventory.Count > 0)
        {
            UpdateFlagsPositions();
        }
    }
    void UpdateFlagsPositions()
    {
        // Assuming there are at least as many Flags as there are distinct item types (1, 2, 3)
        for (int i = 0; i < 3; i++)
        {
            RectTransform rectTransform = Flags[i].GetComponent<RectTransform>();
            if (Inventory[0] == i + 1)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -400);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -500);
            }
        }
    }
    void UseItem()
    {
       

            
        GameObject instantiatedItem = null;
            if (Inventory[0] == 1)
            {
                instantiatedItem = Instantiate(Prefabs[0], transform.position, Quaternion.identity);

            }
            else if (Inventory[0] == 2)
            {
                instantiatedItem = Instantiate(Prefabs[1], transform.position, Quaternion.identity);
            }
            else if (Inventory[0] == 3)
            {
                instantiatedItem = Instantiate(Prefabs[2], transform.position, Quaternion.identity);
            }
            Inventory.RemoveAt(0);

            Rigidbody rb = instantiatedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {

                Vector3 forceDirection = new Vector3(0, 1, 1);
                float forceMagnitude = 500f;
                rb.AddForce(-transform.up * forceMagnitude);
            }
    } 
    void MoveItemToFront (int itemNumber)
    {
       
        List<int> indices = new List<int>();

       
        for (int i = Inventory.Count - 1; i >= 0; i--)
        {
            if (Inventory[i] == itemNumber)
            {
                
                indices.Add(i);
                
                Inventory.RemoveAt(i);
            }
        }

       
        foreach (int index in indices)
        {
            Inventory.Insert(0, itemNumber);
        }
    }
}
