using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Stats
{

    public int baseStat;
    public int currentStat;

    public int GetBaseStat(){ return baseStat; }

    public int GetCurrentStat() { return currentStat; }

    public void IncreaseCurrent(int increment)
    {
        if (increment < 0)
            throw new Exception(""); // TODO MESSAGE EXCEPTION
        if (increment == 0)
            Debug.LogWarning(""); // TODO MESSAGE EXCEPTION
        currentStat += increment;
    }

    public void DecreaseCurrent(int decrement)
    {
        if (decrement < 0)
            throw new Exception(""); // TODO MESSAGE EXCEPTION
        if (decrement == 0)
            Debug.LogWarning(""); // TODO MESSAGE EXCEPTION
        currentStat -= decrement;
        if (currentStat < 0)
            currentStat = 0;
    }

    public void ResetCurrent()
    {
        currentStat = baseStat;
    }

    public static implicit operator Stats(int v)
    {
        throw new NotImplementedException();
    }
}
