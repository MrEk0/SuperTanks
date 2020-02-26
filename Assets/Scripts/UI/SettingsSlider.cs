using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    [SerializeField] Image volumeImage;
    [SerializeField] Sprite volumeOffSprite;

    Sprite volumeOnSprite;
    float minimumVolume;

    private void Awake()
    {
        volumeOnSprite = volumeImage.sprite;
        minimumVolume = GetComponent<Slider>().minValue;
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
}
