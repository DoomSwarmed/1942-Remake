using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform firePoint;
    public Transform player;
    private Rigidbody2D rb;
    public Rigidbody2D bullet;
    private Animator anim;
    private float nextFire = 0f;
    public float fireRate = 1f;
    public float speed = 10f;
    public float moveSpeed;
    private bool isDead = false;
    private Vector2 movement;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        dead();
        Vector3 direction = player.position - transform.position;
        if (Input.GetKeyDown("s") && Time.time > nextFire)
        {
            Shoot();
        }

        direction.Normalize();
        movement = direction;
        move(movement);
    }

    private void FixedUpdate()
    {
        move(movement);
    }

    void move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void Shoot()
    {
        nextFire = Time.time + fireRate;
        var newbullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        newbullet.AddForce(firePoint.up * speed);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            isDead = true;
        }
    }
    void dead()
    {
        if (isDead == true)
        {
            anim.Play("Death");
            StartCoroutine("die");
            
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
