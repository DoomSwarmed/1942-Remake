using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Prefabs")]
    public GameObject deathExplosion;

    [Header("Health")]
    public int maxHealth = 3;
    public int health = 3;

    [Header("Movement")]
    public float moveSpeed = 1.0f;

    float h = 0.0f;
    float v = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h == 0.0f && v == 0.0f)
        {
            Vector3 tilt = Input.acceleration;
            tilt = Quaternion.Euler(45.0f, 0.0f, 0.0f) * tilt;

            h = Mathf.Clamp(tilt.x * 3.0f, -1.0f, 1.0f);
            v = Mathf.Clamp(tilt.y * 3.0f, -1.0f, 1.0f);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(h, v, 0.0f) * moveSpeed;
    }

    void EndGame()
    {
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" || col.tag == "Bullet")
        {
            Destroy(col.gameObject);
            Instantiate(deathExplosion, col.transform.position, Quaternion.identity);
            print("hit");
            health--;
            if (health <= 0)
                EndGame();
        }
    }
}
