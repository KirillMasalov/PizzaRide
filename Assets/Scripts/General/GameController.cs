using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace General
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public int CoinsCount { get; private set; }


        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                LoadData();
            }
            else
                Destroy(this);
        }

        private void LoadData()
        {
            CoinsCount = SavesManager.Instance.LoadCoins();
        }

        public void StartRide()
        {
            SceneTransition.Instance.LoadRideScene();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ChangeCoins(int diff)
        {
            CoinsCount += diff;
            SavesManager.Instance.SaveCoins(CoinsCount);
        }
    }
}