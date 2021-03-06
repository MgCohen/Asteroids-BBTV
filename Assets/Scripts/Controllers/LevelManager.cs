using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int currentWave;
    public int playerLives;

    public Ship playerShip;

    bool isRespawning = false;

    private void OnEnable()
    {
        Services.Register<LevelManager>(this);
    }

    private void OnDisable()
    {
        Services.Unregister<LevelManager>(this);
    }

    [HideInInspector]
    public UnityEvent OnNewWave = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnGameStart = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnLifeLost = new UnityEvent();

    public UnityEvent OnGameEnd = new UnityEvent();

    public void EndWave()
    {
        currentWave++;
        OnNewWave.Invoke();
    }

    public void DestroyShip()
    {
        if (isRespawning) return;
        playerLives--;
        OnLifeLost.Invoke();
        if (playerLives >= 0)
        {
            SpawnShip();
        }
        else
        {
            OnGameEnd.Invoke();
        }
    }

    public void SpawnShip()
    {
        StartCoroutine(RespawnCO());
    }

    IEnumerator RespawnCO()
    {
        isRespawning = true;
        yield return new WaitForSeconds(0.2f);
        while (Physics2D.OverlapCircle(Vector3.zero, 1.5f))
        {
            yield return new WaitForSeconds(0.2f);
        }

        playerShip.transform.position = Vector3.zero;
        playerShip.gameObject.SetActive(true);
        isRespawning = false;
    }

    public void StartGame()
    {
        OnGameStart.Invoke();
        EndWave();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}


