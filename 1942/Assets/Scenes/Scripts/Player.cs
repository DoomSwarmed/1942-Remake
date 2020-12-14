using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer play;

    [Header("Classes")]
    public UI uiClass;

    [Header("Prefabs")]
    public GameObject bullet;
    public GameObject deathExplosion;

    [Header("GameObjects")]
    public GameObject enemyParent;
    public GameObject spawner;
    public GameObject enemyBullet;

    [Header("Health")]
    public int maxHealth = 3;
    public int health = 3;

    [Header("Movement")]
    public float moveSpeed = 1.0f;

    [Header("Joystick")]
    public Transform circle;
    public Transform outerCircle;
    private bool tilter;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    [Header("Stats")]
    public int score = 0;
    public int highScore;
    public int compScore;

    float h = 0.0f;
    float v = 0.0f;

    float shootingCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        play = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HighScore();
        if (health > 0)
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

            if (Input.GetMouseButtonDown(0))
            {
                pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

                circle.transform.position = pointA * 1;
                outerCircle.transform.position = pointA * 1;
                circle.GetComponent<SpriteRenderer>().enabled = true;
                outerCircle.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (Input.GetMouseButton(0))
            {
                touchStart = true;
                pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            }
            else
                touchStart = false;

            if (touchStart)
            {
                Vector2 offset = pointB - pointA;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
                circle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y) * 1;

                h = direction.x;
                v = direction.y;
            }
            else
            {
                circle.GetComponent<SpriteRenderer>().enabled = false;
                outerCircle.GetComponent<SpriteRenderer>().enabled = false;
            }

            shootingCooldown += Time.deltaTime;

            if ((h != 0.0f || v != 0.0f) && shootingCooldown >= 0.25f && gameObject.GetComponent<BoxCollider2D>().enabled == true)
            {
                shootingCooldown = 0.0f;
                ShootBullets();
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(h, v, 0.0f) * moveSpeed;
    }

    void ShootBullets()
    {
        GameObject RightBullet = Instantiate(bullet, transform.position + new Vector3(0.35f, 0.3f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90.0f));
        RightBullet.GetComponent<Projectile>().playerClass = transform.GetComponent<Player>();

        GameObject LeftBullet = Instantiate(bullet, transform.position + new Vector3(-0.35f, 0.3f, 0.0f), Quaternion.Euler(0.0f, 0.0f, -90.0f));
        LeftBullet.GetComponent<Projectile>().playerClass = transform.GetComponent<Player>();
    }

    public void EndGame()
    {
        foreach (Transform enemy in enemyParent.transform)
            Destroy(enemy.gameObject);

        health = maxHealth;
        compScore = score;
        score = 0;
        transform.position = new Vector3(0.0f, -6.0f, 0.0f);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void HighScore()
    {
        compScore = score;
        if (compScore > highScore)
            highScore = compScore;
    }

    IEnumerator RespawnTimer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        circle.GetComponent<SpriteRenderer>().enabled = false;
        outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        uiClass.EndGame();
    }

    IEnumerator Repawn()
    {
        play.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        foreach (Transform enemy in enemyParent.transform)
            Destroy(enemy.gameObject);
        spawner.SetActive(false);
        yield return new WaitForSeconds(1);
        if (health == 0)
            play.enabled = false;
        else
        {
            transform.position = new Vector3(0.0f, -6.0f, 0.0f);
            play.enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(1);
            spawner.SetActive(true);
        }
        
        

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && health > 0 || col.tag == "ebullet")
        {
            Destroy(col.gameObject);
            Instantiate(deathExplosion, col.transform.position, Quaternion.identity);
            health--;
            if (health == 0)
                StartCoroutine(RespawnTimer());
            StartCoroutine(Repawn());
        }
    }
}
