using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Item1,
    Item2,
    Item3,
}

public class Item : MonoBehaviour
{
    public ItemType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Statics.player.inventorySystem.AddItem(gameObject);
        }
    }

}
