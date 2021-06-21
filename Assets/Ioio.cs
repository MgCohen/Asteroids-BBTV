using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ioio : MonoBehaviour
{

    private void Start()
    {
        Down();
    }

    //Safety measures for callback
    private void OnDisable()
    {
        transform.DOKill();
    }

    void Up()
    {
        transform.DOScale(1, 1f).OnComplete(Down);
    }

    void Down()
    {
        transform.DOScale(0.8f, 1f).OnComplete(Up);
    }
}
