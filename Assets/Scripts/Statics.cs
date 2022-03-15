using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public static ThirdPersonController player;

    public static OutStorage activeOutStorage;

    public static void Notification(string text)
    {
        InventorySystem inv = player.GetComponent<InventorySystem>();

        GameObject notification = Instantiate(inv.notificationPrefab, inv.notificationArea.transform);
        notification.GetComponent<TextMeshProUGUI>().text = text;
    }
}
