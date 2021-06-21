using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    [SerializeField] Asteroid asteroidPrefab;
    [SerializeField] List<Sprite> asteroidSprites = new List<Sprite>();
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    LevelManager manager;
    Vector2 playArea;
    int asteroidCount;

    private void OnEnable()
    {
        Services.Register<AsteroidController>(this);
        playArea = PlayArea.Frame;
        manager = Services.Request<LevelManager>();
        manager.OnNewWave.AddListener(SpawnWave);
    }

    private void OnDisable()
    {
        Services.Unregister<AsteroidController>(this);
    }


    public void SpawnAsteroid(Vector3 position, Quaternion rotation, int size)
    {
        var asteroid = Instantiate(asteroidPrefab, position, rotation);
        Sprite sprite = asteroidSprites[Random.Range(0, asteroidSprites.Count)];
        float speed = Random.Range(minSpeed, maxSpeed);
        asteroid.Set(size, sprite, speed);
        asteroidCount++;
    }

    public void SpawnWave()
    {
        //any crazy spawn algorithm
        int wave = manager.currentWave;
        int asteroidCount = 3 + wave;
        for (int i = 0; i < asteroidCount; i++)
        {
            Vector3 point;
            do
            {
                point = RandomPoint();
            }
            while (Vector3.Distance(point, manager.playerShip.transform.position) < 1.5f);
            SpawnAsteroid(point, RandomRotation(), 3);
        }
    }

    public void DestroyAsteroid()
    {
        asteroidCount--;
        if (asteroidCount <= 0)
        {
            manager.EndWave();
        }
    }

    Vector3 RandomPoint()
    {
        return new Vector2(Random.Range(-playArea.x, playArea.x), Random.Range(-playArea.y, playArea.y));
    }

    Quaternion RandomRotation()
    {
        return Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
    }
}
