using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IDestroyable
{
    [Header("Movement")]
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [Header("Manuever")]
    [SerializeField] float rotationSpeed;
    [Header("Firing")]
    [SerializeField] float fireCd;
    float lastFireTime;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPoint;
    [Header("Pulse")]
    [SerializeField] float pulseCd;
    float lastPulseTime;
    [SerializeField] float pulseRange;
    [SerializeField] GameObject pulsePrefab;

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
        if (rotate != 0) Rotate(-rotate); //negative value to rotate to the correct side

        if (Input.GetKey(KeyCode.W))
            Thrust();

        if (Input.GetKeyDown(KeyCode.Space) && CheckFireCD())
        {
            Shot();
        }

        if(Input.GetKeyDown(KeyCode.S) && CheckPulseCD())
        {
            Pulse();
        }
    }

    void Thrust()
    {
        Vector2 velocity = body.velocity + (Time.deltaTime * acceleration * (Vector2)transform.up); //maybe change to addForce
        body.velocity = (velocity.sqrMagnitude > (maxSpeed * maxSpeed)) ? velocity.normalized * maxSpeed : velocity;
    }

    void Rotate(float direction)
    {
        Vector3 rotation = new Vector3(0, 0, Time.deltaTime * rotationSpeed * direction);
        transform.Rotate(rotation);
    }

    bool CheckFireCD()
    {
        return Time.time - lastFireTime > fireCd;
    }

    void Shot()
    {
        Instantiate(bulletPrefab, bulletPoint.position, transform.rotation);
        lastFireTime = Time.time;
    }

    bool CheckPulseCD()
    {
        return Time.time - lastPulseTime > pulseCd;
    }

    void Pulse()
    {
        Instantiate(pulsePrefab, transform);
        lastPulseTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();  
    }

    public void Hit()
    {
        gameObject.SetActive(false);
        manager.DestroyShip();
    }
}
