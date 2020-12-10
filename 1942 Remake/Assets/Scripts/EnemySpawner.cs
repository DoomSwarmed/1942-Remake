using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemySmall;

    public float spawnTime = 5.0f;
    public float spawnTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnTime)
        {
            spawnTimer = Random.Range(0.0f, 2.0f);

            StartCoroutine(spawnPlanes(Random.Range(4, 9)));
        }
    }

    IEnumerator spawnPlanes(int count)
    {
        float xPos = 5.0f;
        float yPos = Random.Range(-5.0f, 5.0f);

        if (Random.Range(0, 2) == 0)
            xPos = -xPos;

        Vector3 spawnPos = new Vector3(xPos, yPos, 0.0f) + new Vector3(0.0f, Random.Range(-1.0f, 1.0f), 0.0f);

        float zRot = 90.0f;

        if (Mathf.Sign(xPos) == 1)
            zRot = -zRot;

        Quaternion spawnRot = Quaternion.Euler(0.0f, 0.0f, zRot + Random.Range(-10.0f, 10.0f));

        int spawnedPlanes = 0;

        while (spawnedPlanes < count)
        {
            Instantiate(enemySmall, spawnPos, spawnRot, transform);
            spawnedPlanes++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
