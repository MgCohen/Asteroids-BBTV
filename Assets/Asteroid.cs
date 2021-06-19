using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    float speed;
    int size;

    [SerializeField]
    SpriteRenderer sprite;

    public void Set(int _size, Sprite _sprite, float _speed)
    {
        size = _size;
        speed = _speed;
        transform.localScale = new Vector3(size, size, 1);
        sprite.sprite = _sprite;
    }

    public void Split()
    {
        var spawner = Services.Request<AsteroidController>();
        spawner.SpawnAsteroid(transform.position, transform.rotation, size - 1);
        spawner.SpawnAsteroid(transform.position, transform.rotation, size - 1);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (size > 1) Split();
    }

}
