using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
