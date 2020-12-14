using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject enemyBullet;

    float fireRate;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 100f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
