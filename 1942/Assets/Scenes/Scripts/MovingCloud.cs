using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    public float cloudSpeed = 100.0f;

    void Start()
    {
        cloudSpeed += Random.Range(-0.5f, 0.5f);
    }

    void Update()
    {
        transform.position += new Vector3(cloudSpeed * Time.deltaTime, 0.0f, 0.0f);

        if (transform.position.x > 1000.0f)
            Destroy(gameObject);
    }
}
