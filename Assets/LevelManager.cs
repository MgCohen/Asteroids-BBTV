using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int currentWave;
    public int playerLives;

    private void OnEnable()
    {
        Services.Register<LevelManager>(this);
    }

    private void OnDisable()
    {
        Services.Unregister<LevelManager>(this);
    }

    public UnityEvent OnWaveEnd = new UnityEvent();

    public void EndWave()
    {
        currentWave++;
        OnWaveEnd.Invoke();
    }
}


