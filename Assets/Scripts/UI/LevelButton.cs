using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LevelButton : MonoBehaviour/*, IPointerClickHandler*/
{
    [SerializeField] Image closedImage;
    [SerializeField] TextMeshProUGUI buttonText;

    private LevelButtonsBehaviour levelButtons;

    public void InstantiateButton(int number, LevelButtonsBehaviour levelButtons)
    {
        buttonText.SetText(number.ToString());
        this.levelButtons = levelButtons;
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (levelButtons == null)
    //        return;

    //    AudioManager.PlayUIButtonAudio();
    //    int sceneIndex = Convert.ToInt32(buttonText.text);
    //    levelButtons.LoadSpecificLevel(sceneIndex);
    //}
    public void LoadLevel()
    {
        if (levelButtons == null)
            return;

        AudioManager.PlayUIButtonAudio();
        int sceneIndex = Convert.ToInt32(buttonText.text);
        levelButtons.LoadSpecificLevel(sceneIndex);
    }

    public void RevealButton()
    {
        GetComponent<Button>().interactable = true;
        closedImage.enabled = false;
    }

    
}
