using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float time;
    [SerializeField] private bool destroy;

    public void Init(Vector3 target, float t, bool destroy, bool local = false, bool returnGravity = true)
    {
        ChangeState(false);
        targetPosition = target;
        time = t;
        if (local) startPosition = transform.localPosition;
        else startPosition = transform.position;
        this.destroy = destroy;
        StartCoroutine(Move(local, returnGravity));
    }

    private IEnumerator Move(bool local, bool returnGravity)
    {
        for (float i = 0; i < 1; i+= (Time.deltaTime / time))
        {
            if (local) transform.localPosition = Vector3.Lerp(startPosition, targetPosition, i);
            else transform.position = Vector3.Lerp(startPosition, targetPosition, i);
            yield return null;
        }
        if (destroy) Destroy(gameObject);
        else
        {
            ChangeState(returnGravity);
            Destroy(this);
        }
    }

    private void ChangeState(bool gravity)
    {
        GetComponent<Rigidbody>().isKinematic = !gravity;
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = gravity;
        }
    }
}
