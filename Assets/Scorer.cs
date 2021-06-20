using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scorer : MonoBehaviour
{

    public int currentScore;

    public UnityEvent OnScore = new UnityEvent();

    private void OnEnable()
    {
        Services.Register<Scorer>(this);
    }

    private void OnDisable()
    {
        Services.Unregister<Scorer>(this);
    }

    public void ScoreAsteroid(int size)
    {
        currentScore += (100 - ((size - 1) * 40));
        OnScore.Invoke();
    }

    public void ScoreSaucer(int size)
    {
        if (size == 1)
            currentScore += 1000;
        else
            currentScore += 200;
        OnScore.Invoke();
    }
}
