using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeUi : MonoBehaviour
{

    LevelManager manager;
    [SerializeField] TextMeshProUGUI lifeText;


    private void OnEnable()
    {
        manager = Services.Request<LevelManager>();
        manager.OnLifeLost.AddListener(UpdateLife);
    }

    private void OnDisable()
    {
        manager.OnLifeLost.RemoveListener(UpdateLife);    
    }

    void UpdateLife()
    {
        lifeText.text = "LIFES: " + manager.playerLives;
    }
}
