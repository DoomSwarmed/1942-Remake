using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionObject : MonoBehaviour
{
    public string deletedTag = "Enemy";
    public bool explode = true;

    public GameObject explosionAnimation;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == deletedTag)
        {
            if (explode)
                Instantiate(explosionAnimation, col.transform.position, Quaternion.identity);

            Destroy(col.gameObject);
        }
    }
}
