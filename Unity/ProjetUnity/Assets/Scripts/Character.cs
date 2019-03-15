using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public Items[] items;
    public Stats healthPoint;
    public Stats attackPoint;
    public Stats movePoint;

    public void GetAttacked(int loss)
    {
        healthPoint.DecreaseCurrent(loss);
    }
    
    public void Move(int used)
    {
        movePoint.DecreaseCurrent(used);
    }

    public void RoundEnded()
    {
        movePoint.ResetCurrent();
    }
}
