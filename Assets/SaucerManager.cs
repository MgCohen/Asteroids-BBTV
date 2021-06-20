using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerManager : MonoBehaviour
{

    Vector2 playArea;
    Scorer score;
    LevelManager manager;

    [SerializeField] List<Saucer> saucers = new List<Saucer>();
    [SerializeField] float spawnTimer;
    float lastSpawnTime;
    public List<Saucer> activeSaucers = new List<Saucer>();

    private void OnEnable()
    {
        playArea = PlayArea.Frame;
        Services.Register<SaucerManager>(this);
        score = Services.Request<Scorer>();
        score.OnScore.AddListener(CheckScore);
        manager = Services.Request<LevelManager>();
        manager.OnNewWave.AddListener(ResetSaucers);
    }

    private void OnDisable()
    {
        Services.Unregister<SaucerManager>(this);
        score.OnScore.RemoveListener(CheckScore);
        manager.OnNewWave.RemoveListener(ResetSaucers);
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime > spawnTimer)
            SpawnSaucer(Random.Range(0, saucers.Count));
    }

    void SpawnSaucer(int size)
    {
        var y = Random.Range(-playArea.y, playArea.y);
        var x = Random.value > 0.5f ? playArea.x : -playArea.x;
        Vector2 spawnPos = new Vector2(x, y);
        var saucer = Instantiate(saucers[size - 1], spawnPos, Quaternion.identity);
        saucer.size = size;
        lastSpawnTime = Time.time;
    }

    void CheckScore()
    {
        if (score.currentScore < 40000) return;

        score.OnScore.RemoveListener(CheckScore);
        saucers.Remove(saucers[1]);
    }

    void ResetSaucers()
    {
        foreach(Saucer saucer in activeSaucers)
        {
            Destroy(saucer.gameObject);
        }
        activeSaucers.Clear();
        lastSpawnTime = Time.time;
        spawnTimer = Mathf.Clamp(spawnTimer - 0.2f, 3, 10);
    }
}
