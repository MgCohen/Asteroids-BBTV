using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saucer : MonoBehaviour, IDestroyable
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float moveRange;
    [SerializeField] float moveVariance;
    public int size;

    Vector2 playArea;
    float spawnTime;
    float lifeTime = 3;

    SaucerManager saucerManager;


    private void Start()
    {
        saucerManager = Services.Request<SaucerManager>();
        saucerManager.activeSaucers.Add(this);
        spawnTime = Time.time;
        playArea = PlayArea.Frame;
        Vector2 initialDirection = transform.position.x > 0 ? Vector2.left : Vector2.right;
        Move(initialDirection * moveRange);
    }

    void Move(Vector3 direction)
    {
        if (Mathf.Abs(transform.position.x) > playArea.x || (Mathf.Abs(transform.position.y) > playArea.y && CheckLifeTime()))
        {
            Hit();
            return;
        }
        float angleVariance = Random.Range(-moveVariance, moveVariance);
        Vector3 nextDirection = Quaternion.AngleAxis(angleVariance, Vector3.forward) * direction;
        float speed = Random.Range(minSpeed, maxSpeed);
        transform.DOMove(transform.position + direction, speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => Move(nextDirection));
    }

    bool CheckLifeTime()
    {
        return Time.time - spawnTime > lifeTime;
    }

    public void Hit()
    {
        Destroy(gameObject);
        saucerManager.activeSaucers.Remove(this);
        Services.Request<Scorer>().ScoreSaucer(size);
    }

    //safety measures for tween callback
    private void OnDisable()
    {
        transform.DOKill();
    }

}
