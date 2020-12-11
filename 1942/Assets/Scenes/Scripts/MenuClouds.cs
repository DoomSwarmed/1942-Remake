using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClouds : MonoBehaviour
{
    public GameObject cloudPrefab;

    void Start()
    {
        StartCoroutine(MoveClouds());
    }

    IEnumerator MoveClouds()
    {
        while (true)
        {
            Instantiate(cloudPrefab, new Vector3(-650.0f, Random.Range(200.0f, 1000.0f), 0.0f), Quaternion.identity, transform);
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        }
    }
}
