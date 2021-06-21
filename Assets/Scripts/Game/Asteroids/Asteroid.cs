using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, IDestroyable
{

    float speed;
    int size;

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
        controller.SpawnAsteroid(transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, Random.Range(-25, 25))), size - 1);
        controller.SpawnAsteroid(transform.position, transform.rotation * Quaternion.Euler(new Vector3(0, 0, Random.Range(-25, 25))), size - 1);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    public void Hit()
    {
        col.enabled = false;
        Destroy(gameObject);
        controller.DestroyAsteroid();
        Services.Request<Scorer>().ScoreAsteroid(size);
        if (size > 1) Split();
    }

}
