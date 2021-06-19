using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [Header("Manuever")]
    [SerializeField] float rotationSpeed;
    [Header("Firing")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPoint;

    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float rotate = Input.GetAxisRaw("Horizontal");
        if (rotate != 0) Rotate();

        if (Input.GetKey(KeyCode.W))
            Thrust();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
    }

    public void Thrust()
    {
        Vector2 velocity = body.velocity + (Time.deltaTime * acceleration * (Vector2)transform.up);
        body.velocity = (velocity.sqrMagnitude > Mathf.Sqrt(maxSpeed)) ? velocity.normalized * maxSpeed : velocity;
    }

    public void Rotate()
    {
        Vector3 rotation = new Vector3(0, 0, Time.deltaTime * rotationSpeed);
        transform.Rotate(rotation);
    }

    public void Shot()
    {
        Instantiate(bulletPrefab, bulletPoint.position, transform.rotation);
    }
}
