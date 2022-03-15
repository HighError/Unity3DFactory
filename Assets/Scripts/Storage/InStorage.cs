using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class InStorage : MonoBehaviour
{
    public GameObject[] items;
    public int[] count;
    public int[] maxCount;

    private Coroutine coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && other.isTrigger)
        {
            Item item = other.gameObject.GetComponent<Item>();
            if (items.FirstOrDefault(x => x.GetComponent<Item>().type == item.type) != null)
            {
                int inItem = Array.FindIndex(items, x => x.GetComponent<Item>().type == item.type);
                if (count[inItem] < maxCount[inItem])
                {
                    Destroy(other.gameObject);
                    count[inItem]++;
                }
                
            }
        }
        if (other.CompareTag("Player"))
        {
            Statics.player.info.gameObject.SetActive(true);
            coroutine = StartCoroutine(UpdateUI());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(coroutine);
            Statics.player.info.gameObject.SetActive(false);
        }
    }

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            Statics.player.info.text = "";
            for (int i = 0; i < items.Length; i++)
            {
                Statics.player.info.text += $"{items[i].GetComponent<Item>().type}: {count[i]}/{maxCount[i]}\n";
            }
            yield return new WaitForSeconds(1);
        }
    }
}
