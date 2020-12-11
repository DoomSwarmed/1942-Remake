using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed = 1.0f;
    public float rotationSpeed = 90.0f;

    public float rotationTimer = 1.0f;
    public bool rotatingRight = true;

    float timeUntilRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (Random.Range(0, 2) == 0)
            rotatingRight = !rotatingRight;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        UpdateRotation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = -transform.up * moveSpeed;
    }

    void Rotate()
    {
        if (rotatingRight)
            transform.localEulerAngles += new Vector3(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
        else
            transform.localEulerAngles += new Vector3(0.0f, 0.0f, -rotationSpeed * Time.deltaTime);
    }

    void UpdateRotation()
    {
        timeUntilRotation += Time.deltaTime;
        if (timeUntilRotation >= rotationTimer)
        {
            timeUntilRotation = 0.0f;
            if (Random.Range(0, 2) == 0)
                rotatingRight = true;
            else
                rotatingRight = false;
        }
    }
}
