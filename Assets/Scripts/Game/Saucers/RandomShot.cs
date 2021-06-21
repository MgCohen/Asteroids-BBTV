using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShot : MonoBehaviour
{
    [SerializeField] float fireCD;
    float lastFireTime;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] Transform gunPoint;

    void Update()
    {
        if (Time.time - lastFireTime < fireCD) return;

        Aim();
        Fire();
        lastFireTime = Time.time;
    }

    void Aim()
    {
        var angle = Random.Range(0f, 360f);
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Fire()
    {
        Instantiate(bullet, gunPoint.position, gun.rotation);
    }
}
