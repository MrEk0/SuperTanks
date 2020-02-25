using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Test : MonoBehaviour
{
    public AudioClip engineSound;
    public AudioClip workingEmgine;

    AudioSource tankSource;

    private void Awake()
    {
        tankSource = gameObject.AddComponent<AudioSource>() as AudioSource;
    }

    public void PlayEngineSound()
    {
        //tankSource=GetComponent<AudioSource>();
        tankSource.clip = engineSound;
        tankSource.loop = true;
        tankSource.Play();
    }
}
