using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public float soundVolume;
    public float musicVolume;
    public int levelProgress;
    public int numberOfOpenedLevels;

    public PlayerData(float soundVolume, float musicVolume)
    {
        this.soundVolume = soundVolume;
        this.musicVolume = musicVolume;
    }

    public PlayerData(int levelProgress/*, int openedLevels*/)
    {
        this.levelProgress = levelProgress;
        //numberOfOpenedLevels = openedLevels;
    }
}
