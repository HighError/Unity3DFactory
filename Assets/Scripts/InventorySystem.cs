using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventorySystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject notificationPrefab;
    public GameObject notificationArea;
    public List<GameObject> items;
    private int maxCount = 5;

    public void AddItem(GameObject item)
    {
        if (items.Count < maxCount)
        {
            item.transform.parent = transform;
            item.AddComponent<ItemAnimation>().Init(new Vector3(0, (0.4f + 0.3f * items.Count), -0.2f),1f, false, true, false);
            items.Add(item);
        }
    }

    public void DropItem()
    {
        if (items.Count > 0)
        {
            GameObject item = items.Last();
            items.Remove(items.Last());
            item.transform.parent = null;
            item.AddComponent<ItemAnimation>().Init(new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z + 0.5f), 1f, false, false, true);

        }
    }

    public void GetItemFromOut()
    {
        if (Statics.activeOutStorage != null)
        {
            Statics.activeOutStorage.OutItem();
        }
    }
}
