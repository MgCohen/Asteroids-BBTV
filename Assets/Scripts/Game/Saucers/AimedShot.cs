using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedShot : MonoBehaviour
{
    [SerializeField] float fireCD;
    float lastFireTime;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] Transform gunPoint;
    [SerializeField] float aimVariance;
    [SerializeField] float accuracyPerWave;

    Ship player;
    int wave;

    private void Start()
    {
        var manager = Services.Request<LevelManager>();
        player = manager.playerShip;
        wave = manager.currentWave;
    }

    void Update()
    {
        if (Time.time - lastFireTime < fireCD) return;

        Aim();
        Fire();
        lastFireTime = Time.time;
    }

    void Aim()
    {
        Vector2 direction = player.transform.position - transform.position;
        float variance = Mathf.Clamp(aimVariance - (wave * accuracyPerWave), 0, aimVariance);
        direction = Quaternion.AngleAxis(Random.Range(-variance, variance), Vector3.forward) * direction;
        gun.up = direction;
    }

    void Fire()
    {
        Instantiate(bullet, gunPoint.position, gun.rotation);
    }
}
