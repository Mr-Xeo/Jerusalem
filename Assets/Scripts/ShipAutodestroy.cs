using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAutodestroy : MonoBehaviour
{
    public float TimeBeforeDestroying;
    public float goSpeed;

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    void FixedUpdate()
    {
        transform.position += Vector3.right * goSpeed * Time.deltaTime;
    }

    IEnumerator DestroyCoroutine()
    {

        yield return new WaitForSeconds(TimeBeforeDestroying);
        Destroy(gameObject);
    }
}
