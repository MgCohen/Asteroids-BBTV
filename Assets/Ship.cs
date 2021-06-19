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
    LevelManager manager;

    private void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
        manager = Services.Request<LevelManager>();
        manager.playerShip = this;
    }

    private void Update()
    {
        float rotate = Input.GetAxisRaw("Horizontal");
        if (rotate != 0) Rotate(rotate);

        if (Input.GetKey(KeyCode.W))
            Thrust();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }

        Debug.Log(body.velocity.magnitude);
    }

    public void Thrust()
    {
        Vector2 velocity = body.velocity + (Time.deltaTime * acceleration * (Vector2)transform.up);
        body.velocity = (velocity.sqrMagnitude > (maxSpeed * maxSpeed)) ? velocity.normalized * maxSpeed : velocity;
    }

    public void Rotate(float direction)
    {
        Vector3 rotation = new Vector3(0, 0, Time.deltaTime * rotationSpeed * direction);
        transform.Rotate(rotation);
    }

    public void Shot()
    {
        Instantiate(bulletPrefab, bulletPoint.position, transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //check respawn
    }
}
