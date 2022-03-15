using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyUI : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DestoyObject());
    }

    private IEnumerator DestoyObject()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
