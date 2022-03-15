using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private InStorage inStorages;
    [SerializeField] private OutStorage outStorage;
    [SerializeField] private Vector3 targetPosition;

    private int count;


    private void Start()
    {
        StartCoroutine(CreateItemTimer());
        StartCoroutine(ItemToOutStorageTimer());
    }

    private IEnumerator CreateItemTimer()
    {
        float duration = 3f;
        float durationMove = 3f;
        while (true)
        {
            bool activate = true;
            if (FullOutStorage())
            {
                activate = false;
                Statics.Notification($"{gameObject.name}: Out storage full!");
            }
            else if ((inStorages != null && !ContainsItems()))
            {
                activate = false;
                Statics.Notification($"{gameObject.name}: Not enought resources!");
            }

            if (activate && inStorages != null)
            {
                for (int i = 0; i < inStorages.count.Length; i++)
                {
                    GameObject obj = Instantiate(inStorages.items[i], new Vector3(inStorages.gameObject.transform.position.x, 0.85f, inStorages.gameObject.transform.position.z), new Quaternion(0,0,0,0));
                    obj.AddComponent<ItemAnimation>().Init(transform.position, duration, true);
                    inStorages.count[i]--;
                }
            }

            for (float i = 0; i < 1; i += Time.deltaTime / (durationMove + duration))
            {
                yield return null;
            }

            if (activate) count++;
        }
    }

    private IEnumerator ItemToOutStorageTimer()
    {
        float duration = 3f;
        while (true)
        {
            bool activate = !(count == 0);

            if (activate)
            {
                GameObject obj = Instantiate(outStorage.item, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion(0,0,0,0));
                obj.AddComponent<ItemAnimation>().Init(targetPosition, duration, true);
            }

            for (float i = 0; i < 1; i += (Time.deltaTime / duration))
            {
                yield return null;
            }

            if (activate)
            {
                count--;
                outStorage.count++;
            }
        }
    }

    private bool ContainsItems()
    {
        foreach (var item in inStorages.count)
        {
            if (item == 0)
            {
                return false;
            }
        }
        return true;
    }

    private bool FullOutStorage()
    {
        if (outStorage.count + count == outStorage.maxCount)
        {
            return true;
        }
        return false;
    }
}
