using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShipGenerator : MonoBehaviour
{

    public GameObject prefabShip;
    public GameObject generator;
    private bool isShipSpawning;

    void Update()
    {
        if(!isShipSpawning)
        {
            StartCoroutine(ShipSpawner());
        }
    }


    IEnumerator ShipSpawner()
    {
        isShipSpawning = true;

        var randomTime = 0;
        randomTime = Random.Range(45,180);

        yield return new WaitForSeconds(randomTime);

        Instantiate(prefabShip, generator.transform.position, prefabShip.transform.rotation);
        prefabShip.transform.localScale = new Vector3(50,50);
        isShipSpawning = false;
    }
}
