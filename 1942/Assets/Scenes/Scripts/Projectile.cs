using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    public Player playerClass;

    public float moveSpeed = 10.0f;

    public GameObject explosionAnimation;

    float lifetime = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5.0f)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            playerClass.score += 50;
            Instantiate(explosionAnimation, col.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180.0f, 180.0f)));
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
