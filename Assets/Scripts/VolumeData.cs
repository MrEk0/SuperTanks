using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VolumeData
{
    public float soundVolume;
    public float musicVolume;

    public VolumeData(float soundVolume, float musicVolume)
    {
        this.soundVolume = soundVolume;
        this.musicVolume = musicVolume;
    }
}
