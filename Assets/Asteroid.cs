using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    float speed;
    int size;
    bool destroyed = false;

    AsteroidController controller;


    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    CircleCollider2D col;


    private void OnEnable()
    {
        controller = Services.Request<AsteroidController>();
    }

    public void Set(int _size, Sprite _sprite, float _speed)
    {
        size = _size;
        speed = _speed;
        transform.localScale = new Vector3(size, size, 1);
        sprite.sprite = _sprite;
    }

    public void Split()
    {
        controller.SpawnAsteroid(transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, Random.Range(-15, 15))), size - 1);
        controller.SpawnAsteroid(transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, Random.Range(-15, 15))), size - 1);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        col.enabled = false;
        Destroy(gameObject);
        controller.DestroyAsteroid();
        if (size > 1) Split();
    }

}
