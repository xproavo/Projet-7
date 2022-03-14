using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStockage : MonoBehaviour
{
    public List<GameObject> InventoryGameObject;

    public void AddInInventory(GameObject objectPick)
    {
        InventoryGameObject.Add(objectPick);
    }

    public GameObject GetObject(int index)
    {
        return InventoryGameObject[index];
    }

    public List<GameObject> GetAllInventory()
    {
        return InventoryGameObject;
    }

    public void DeleteAObject(int index)
    {
        InventoryGameObject.RemoveAt(index);
    }
}
