using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    bool hitted = false;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitted) return;
        hitted = true;
        Destroy(gameObject);
        var hit = collision.gameObject.GetComponent<IDestroyable>();
        if(hit != null)
        {
            hit.Hit();
        }
    }
}
