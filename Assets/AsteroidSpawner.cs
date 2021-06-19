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

    public void SpawnWave(int wave)
    {
        //any crazy spawn algorithm
        int asteroidCount = 5 + wave;
        for (int i = 0; i < asteroidCount; i++)
        {
            SpawnAsteroid(RandomPoint(), RandomRotation(), 3);
        }
    }

    public void DestroyAsteroid()
    {
        asteroidCount--;
        if(asteroidCount <= 0)
        {
            if (manager == null) manager = Services.Request<LevelManager>();
            manager.currentWave++;
            SpawnWave(manager.currentWave);
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
