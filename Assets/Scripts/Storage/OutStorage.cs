using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutStorage : MonoBehaviour
{
    public GameObject item;
    public int count;
    public int maxCount;

    private Coroutine coroutine;

    public void OutItem ()
    {
        if (count > 0)
        {
            Instantiate(item, new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z), new Quaternion(0,0,0,0));
            count--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Statics.player.ChangeActiveOutStorage(this); 
            Statics.player.info.gameObject.SetActive(true);
            coroutine = StartCoroutine(UpdateUI());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Statics.player.ChangeActiveOutStorage(null);
            StopCoroutine(coroutine);
            Statics.player.info.gameObject.SetActive(false);
        }
    }

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            Statics.player.info.text = $"{item.GetComponent<Item>().type}: {count}/{maxCount}";
            yield return new WaitForSeconds(1);
        }
    }
}
