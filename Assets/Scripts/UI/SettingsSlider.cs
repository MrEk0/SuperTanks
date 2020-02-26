using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    [SerializeField] Image volumeOnImage;
    [SerializeField] Sprite volumeOffSprite;

    Sprite startSprite;

    float minimumVolume;

    private void Awake()
    {
        startSprite = volumeOnImage.sprite;

        minimumVolume = GetComponent<Slider>().minValue;
    }

    public void SetImage(float volume)
    {
        if (volume <= minimumVolume)
        {
            volumeOnImage.sprite = volumeOffSprite;
        }
        else
        {
            volumeOnImage.sprite = startSprite;
        }
    }
}
