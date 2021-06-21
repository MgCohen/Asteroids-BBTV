using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    Ship player;

    private void Start()
    {
        player = Services.Request<LevelManager>().playerShip;
    }

    public void IncreaseFireRatio()
    {
        player.fireCd -= 0.02f;
    }

    public void IncreaseSpeed()
    {
        player.maxSpeed += 0.1f;
    }

    public void IncreasePulseRange()
    {
        player.pulseRange += 0.3f;
    }

    public void IncreasePulseRatio()
    {
        player.pulseCd -= 0.02f;
    }
}
