using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SliderType
{
    Music,
    Sound
}

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] SliderType sliderType;
    [SerializeField] Image volumeImage;
    [SerializeField] Sprite volumeOffSprite;

    Slider slider;
    Sprite volumeOnSprite;
    float minimumVolume;

    private void OnEnable()
    {
        AudioManager.onVolumeChanged += ChangeSliderValue;
    }

    private void OnDisable()
    {
        AudioManager.onVolumeChanged -= ChangeSliderValue;
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        volumeOnSprite = volumeImage.sprite;
        minimumVolume = slider.minValue;
    }

    public void SetVolumeImage(float volume)
    {
        if(volume<=minimumVolume)
        {
            volumeImage.sprite = volumeOffSprite;
        }
        else
        {
            volumeImage.sprite = volumeOnSprite;
        }
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.SetMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        AudioManager.SetSoundVolume(volume);
    }

    private void ChangeSliderValue(float musicVolume, float soundVolume)
    {
        if (sliderType == SliderType.Music)
        {
            slider.value = musicVolume;
        }
        else if (sliderType == SliderType.Sound)
        {
            slider.value = soundVolume;
        }
    }
}
