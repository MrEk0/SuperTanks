using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using SuperTanks.Core;

namespace SuperTanks.UI
{
    public class LivesUI : MonoBehaviour
    {
        List<Image> lifeImages;
        int lifeNumber;
        private void Awake()
        {
            lifeImages = GetComponentsInChildren<Image>().ToList();
            lifeNumber = lifeImages.Count;
        }

        public void DecreaseLifeNumber()
        {
            if (lifeNumber > 0f)
            {
                lifeNumber--;

                Image image = lifeImages[lifeNumber];
                image.color = new Color(1f, 1f, 1f, image.color.a / 2f);
            }

            if (lifeNumber == 0)
            {
                AudioManager.PlayGameOverAudio();
            }
        }
    }
}

