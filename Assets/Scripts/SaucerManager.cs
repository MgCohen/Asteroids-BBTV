using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerManager : MonoBehaviour
{

    Scorer score;
    LevelManager manager;

    [SerializeField] List<Saucer> saucers = new List<Saucer>();
    [SerializeField] float spawnTimer;
    public List<Saucer> activeSaucers = new List<Saucer>();
    float currentSpawnTimer;
    float lastSpawnTime;
    Vector2 playArea;
    bool started = false;

    private void OnEnable()
    {
        playArea = PlayArea.Frame;
        Services.Register<SaucerManager>(this);
        score = Services.Request<Scorer>();
        score.OnScore.AddListener(CheckScore);
        manager = Services.Request<LevelManager>();
        manager.OnNewWave.AddListener(ResetSaucers);
        manager.OnGameStart.AddListener(StartSaucers);
        currentSpawnTimer = spawnTimer;
    }

    private void OnDisable()
    {
        Services.Unregister<SaucerManager>(this);
        score.OnScore.RemoveListener(CheckScore);
        manager.OnNewWave.RemoveListener(ResetSaucers);
        manager.OnGameStart.RemoveListener(StartSaucers);
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime > spawnTimer && started)
            SpawnSaucer(Random.Range(1, saucers.Count));
    }

    void StartSaucers()
    {
        lastSpawnTime = Time.time;
        started = true;
    }

    void SpawnSaucer(int size)
    {
        var y = Random.Range(-playArea.y, playArea.y);
        var x = Random.value > 0.5f ? playArea.x : -playArea.x;
        Vector2 spawnPos = new Vector2(x, y);
        Debug.Log(1);
        var saucer = Instantiate(saucers[size - 1], spawnPos, Quaternion.identity);
        saucer.size = size;
        Debug.Log(2);
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
        currentSpawnTimer = Mathf.Clamp(currentSpawnTimer - 0.2f, 3, spawnTimer);
    }
}
