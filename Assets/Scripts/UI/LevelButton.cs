using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Image closedImage;

    //bool isOpened = false;

    public void RevealButton()
    {

        GetComponent<Button>().interactable = true;
        closedImage.enabled = false;
    }
}
