using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Pulse : MonoBehaviour
{
    public void Set(float range)
    {
        var radius = 0.1f * range;
        transform.DOScale(radius, 2).SetSpeedBased().OnComplete(() => Destroy(gameObject));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject.GetComponent<IDestroyable>();
        if (hit != null) hit.Hit();
    }
}
