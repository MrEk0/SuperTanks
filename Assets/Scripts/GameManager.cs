using SuperTanks.Saving;
using SuperTanks.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperTanks.Core
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;

        [SerializeField] int numberOfLevels = 5;
        [SerializeField] int gamesToShowAds = 1;

        private int numberOfGame = 0;
        Ads ads;

        public static int NumberOfOpenedLevels { get; private set; } = 0;
        public static List<GameObject> levelButtons { get; set; }
        public static bool IsGamePause { get; private set; } = false;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            ads = GetComponent<Ads>();
        }

        private void Start()
        {
            LoadProgress();
        }

        public static int GetLevelNumber()
        {
            return instance.numberOfLevels;
        }

        public static void PauseGame()
        {
            if (instance == null)
                return;

            IsGamePause = true;
            AudioManager.StopAllTankSounds();
        }

        public static void ResumeGame()
        {
            if (instance == null)
                return;

            IsGamePause = false;
        }

        public static void SaveProgress()
        {
            if (instance == null)
                return;

            float sound = AudioManager.SoundVolume;
            float music = AudioManager.MusicVolume;
            DataSaver.Save(sound, music, NumberOfOpenedLevels);
        }

        public static void LoadProgress()
        {
            if (instance == null)
                return;

            PlayerData progress = DataSaver.Load();

            if (progress != null)
            {
                NumberOfOpenedLevels = progress.levelProgress;
            }
            OpenNewLevels();
        }

        public static void LevelUp(int sceneIndex)
        {
            if (instance == null)
                return;

            if (sceneIndex <= NumberOfOpenedLevels)
                return;

            NumberOfOpenedLevels++;
            SaveProgress();
        }

        public static void OpenNewLevels()
        {
            if (instance == null)
                return;

            for (int i = 0; i <= NumberOfOpenedLevels; i++)
            {
                if (levelButtons != null && levelButtons.Contains(levelButtons[i]))
                    levelButtons[i].GetComponent<LevelButton>().RevealButton();
            }
        }

        public static void ShowAds()
        {
            instance.numberOfGame++;

            if (instance.numberOfGame % instance.gamesToShowAds == 0) 
            {
                instance.ads.ShowVideoAds();
            }
        }
    }
}
