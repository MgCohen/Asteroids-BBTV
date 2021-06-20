using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUi : MonoBehaviour
{

    Scorer scoreManager;
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        scoreManager = Services.Request<Scorer>(true);
        scoreManager.OnScore.AddListener(UpdateScore);
    }

    private void OnDisable()
    {
        scoreManager.OnScore.RemoveListener(UpdateScore);
    }

    public void UpdateScore()
    {
        scoreText.text = scoreManager.currentScore.ToString();
    }
}
