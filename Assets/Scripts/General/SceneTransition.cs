using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance { get; private set; }
        private const string RIDESCENE_NAME = "RideScene";
        private const string MAINMENU_NAME = "MainMenu";
        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(this);
        }

        public void LoadRideScene()
        {
            SceneManager.LoadScene(RIDESCENE_NAME);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MAINMENU_NAME);
            Time.timeScale = 1;
        }
    }
}
