using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject backgroundObject;
    public GameObject lastSpawnedBackground;

    public float scrollSpeed = 6.0f;

    // Update is called once per frame
    void Update()
    {
        foreach (Transform ocean in transform)
        {
            ocean.position -= new Vector3(0.0f, scrollSpeed * Time.deltaTime, 0.0f);

            if (ocean.position.y <= -18.0f)
                Destroy(ocean.gameObject);
        }

        if (lastSpawnedBackground.transform.position.y <= 0.0f)
        {
            GameObject newBackground = Instantiate(backgroundObject, lastSpawnedBackground.transform.position + new Vector3(0.0f, 18.0f, 0.0f), Quaternion.identity, transform);
            lastSpawnedBackground = newBackground;
        }
    }
}
