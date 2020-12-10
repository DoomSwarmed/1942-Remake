using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionTimer : MonoBehaviour
{
    public float destructionTime = 0.5f;
    float destructionTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        destructionTimer += Time.deltaTime;

        if (destructionTimer >= destructionTime)
            Destroy(gameObject);
    }
}
