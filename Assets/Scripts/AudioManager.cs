using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//namespace Assets.Scripts
//{
class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    [Header("Ambient Clips")]
    public AudioClip mainMenuClip;
    public AudioClip clickUIClip;
    public AudioClip swipeUIClip;

    [Header("Tank Clips")]
    public AudioClip playerEngineClip;
    public AudioClip enemyEngineClip;
    public AudioClip rocketExplosionClip;
    public AudioClip tankExplosionClip;
    public AudioClip fireClip;
    public AudioClip hitImpactClip;
    public AudioClip pickUpClip;

    [Header("Voice Clips")]
    public AudioClip gameOverClip;
    public AudioClip congratulationsClip;
    public AudioClip readyClip;
    public AudioClip goClip;

    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup soundGroup;
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup enemyGroup;
    public AudioMixerGroup voiceGroup;
    public AudioMixerGroup stringGroup;

    AudioSource musicSource;
    AudioSource tankPlayerSource;
    AudioSource tankEnemySource;
    AudioSource playerTankActivitySource;
    AudioSource enemyTankActivitySource;
    AudioSource voiceSource;
    AudioSource rocketSource;
    AudioSource stingSource;

    //float musicVolume;
    //float soundVolume;
    public static float MusicVolume { get; private set; }
    public static float SoundVolume { get; private set; }

    public static event Action<float, float> onVolumeChanged;
    //public event Action<float> onSoundChanged;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        tankEnemySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        tankPlayerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerTankActivitySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        enemyTankActivitySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        rocketSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        musicSource.outputAudioMixerGroup = musicGroup;
        tankPlayerSource.outputAudioMixerGroup = playerGroup;
        tankEnemySource.outputAudioMixerGroup = enemyGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;
        playerTankActivitySource.outputAudioMixerGroup = playerGroup;
        enemyTankActivitySource.outputAudioMixerGroup = enemyGroup;
        rocketSource.outputAudioMixerGroup = musicGroup;
        stingSource.outputAudioMixerGroup = stringGroup;

        StartLevelAudio();
        //LoadVolume();
    }

    private void Start()
    {
        LoadVolume();
    }

    private void StartLevelAudio()
    {
        instance.musicSource.clip = mainMenuClip;
        instance.musicSource.loop = true;
        instance.musicSource.Play();
    }

    public static void StopAllTankSounds()
    {
        instance.tankEnemySource.Stop();
        instance.tankPlayerSource.Stop();
    }

    public static void PlayPlayerEngineAudio()
    {
        if (instance == null)
            return;

        if (instance.tankPlayerSource.isPlaying)
            return;

        instance.tankPlayerSource.clip = instance.playerEngineClip;
        instance.tankPlayerSource.loop = true;
        instance.tankPlayerSource.Play();
    }

    public static void PlayEnemyEngineAudio()
    {
        if (instance == null)
            return;

        if (instance.tankEnemySource.isPlaying)
            return;

        instance.tankEnemySource.clip = instance.enemyEngineClip;
        instance.tankEnemySource.loop = true;
        instance.tankEnemySource.Play();
    }

    public static void PlayGameOverAudio()
    {
        if (instance == null)
            return;

        StopAllTankSounds();
        //instance.musicSource.Stop();

        instance.voiceSource.clip = instance.gameOverClip;
        instance.voiceSource.Play();
    }

    public static void PlayCongratulationsAudio()
    {
        if (instance == null)
            return;

        StopAllTankSounds();
        //instance.musicSource.Stop();//!!!

        instance.voiceSource.clip = instance.congratulationsClip;
        instance.voiceSource.Play();
    }

    public static void PlayUIButtonAudio()//!!!!!
    {
        if (instance == null)
            return;

        instance.stingSource.clip = instance.clickUIClip;
        instance.stingSource.Play();
    }

    public static void PlayUISwipeAudio()
    {
        if (instance == null)
            return;

        instance.stingSource.clip = instance.swipeUIClip;
        instance.stingSource.Play();
    }

    public static void PlayPlayerExplosionAudio()
    {
        if (instance == null)
            return;

        instance.tankPlayerSource.clip = instance.tankExplosionClip;
        instance.tankPlayerSource.loop = false;
        instance.tankPlayerSource.Play();
    }

    public static void PlayEnemyExplosionAudio()
    {
        if (instance == null)
            return;

        instance.tankEnemySource.clip = instance.tankExplosionClip;
        instance.tankEnemySource.loop = false;
        instance.tankEnemySource.Play();
    }

    public static void PlayEnemyFireAudio()
    {
        if (instance == null)
            return;

        instance.enemyTankActivitySource.clip = instance.fireClip;
        instance.enemyTankActivitySource.Play();
    }

    public static void PlayPlayerFireAudio()
    {
        if (instance == null)
            return;

        instance.playerTankActivitySource.clip = instance.fireClip;
        instance.playerTankActivitySource.Play();
    }

    public static void PlayRocketAudio()
    {
        if (instance == null)
            return;

        instance.rocketSource.clip = instance.rocketExplosionClip;
        instance.rocketSource.Play();
    }

    public static void PlayEnemyHitAudio()
    {
        if (instance == null)
            return;

        instance.enemyTankActivitySource.clip = instance.hitImpactClip;
        instance.enemyTankActivitySource.Play();
    }

    public static void PlayPlayerHitAudio()
    {
        if (instance == null)
            return;

        instance.playerTankActivitySource.clip = instance.hitImpactClip;
        instance.playerTankActivitySource.Play();
    }

    public static void PlayPickUpAudio()
    {
        if (instance == null)
            return;

        instance.stingSource.clip = instance.pickUpClip;
        instance.stingSource.Play();
    }

    public static void PlayReadyGoAudio()
    {
        instance.StartCoroutine(instance.PlayReadyGoSound());
    }

    IEnumerator PlayReadyGoSound()
    {
        instance.voiceSource.clip = instance.readyClip;
        instance.voiceSource.Play();

        while (instance.voiceSource.isPlaying)
        {
            yield return null;
        }

        instance.voiceSource.clip = instance.goClip;
        instance.voiceSource.Play();

        while (instance.voiceSource.isPlaying)
        {
            yield return null;
        }
        GameManager.instance.ResumeGame();
    }

    public static void SetSoundVolume(float volume)
    {
        SoundVolume = volume;
        instance.soundGroup.audioMixer.SetFloat("Sound", volume);       
    }

    public static void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        instance.musicGroup.audioMixer.SetFloat("Music", volume);
    }

    public static void LoadVolume()
    {
        PlayerData data = DataSaver.Load();

        if (data != null)
        {
            MusicVolume = data.musicVolume;
            SoundVolume = data.soundVolume;

            Debug.Log("Load M  " + MusicVolume);
            Debug.Log("Load S  " + SoundVolume);
            Debug.Log("Load P  " + GameManager.instance.NumberOfOpenedLevels);

            SetMusicVolume(MusicVolume);
            SetSoundVolume(SoundVolume);

            onVolumeChanged(MusicVolume, SoundVolume);
        }
    }
}
//}
