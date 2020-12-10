using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Prefabs")]
    public GameObject bullet;
    public GameObject deathExplosion;

    [Header("GameObjects")]
    public GameObject enemyParent;

    [Header("Health")]
    public int maxHealth = 3;
    public int health = 3;

    [Header("Movement")]
    public float moveSpeed = 1.0f;

    [Header("Stats")]
    public int score = 0;

    float h = 0.0f;
    float v = 0.0f;

    float shootingCooldown = 0.0f;

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

        shootingCooldown += Time.deltaTime;

        if ((h != 0.0f || v != 0.0f) && shootingCooldown >= 0.25f)
        {
            shootingCooldown = 0.0f;
            ShootBullets();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(h, v, 0.0f) * moveSpeed;
    }

    void ShootBullets()
    {
        GameObject RightBullet = Instantiate(bullet, transform.position + new Vector3(0.35f, 0.3f, 0.0f), Quaternion.identity);
        RightBullet.GetComponent<Projectile>().playerClass = transform.GetComponent<Player>();

        GameObject LeftBullet = Instantiate(bullet, transform.position + new Vector3(-0.35f, 0.3f, 0.0f), Quaternion.identity);
        LeftBullet.GetComponent<Projectile>().playerClass = transform.GetComponent<Player>();
    }

    void EndGame()
    {
        foreach (Transform enemy in enemyParent.transform)
            Destroy(enemy.gameObject);

        health = maxHealth;
        score = 0;
        transform.position = new Vector3(0.0f, -6.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Instantiate(deathExplosion, col.transform.position, Quaternion.identity);
            health--;
            if (health <= 0)
                EndGame();
        }
    }
}
