using System;
using System.Collections;
using System.Collections.Generic;
using UI.Ride.Animations;
using UI.Ride.Panels;
using UnityEngine;

namespace UI.Ride
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance { get; private set; }

        [SerializeField] private PauseMenuAnimationController pauseMenuAnimations;
        [SerializeField] private WinPanel winPanel;
        [SerializeField] private LosePanel losePanel;
        private void Awake()
        {
            Instance = this;
        }

        public void OpenPauseMenu()
        {
            pauseMenuAnimations.OpenMenu();
        }

        public void ClosePauseMenu()
        {
            pauseMenuAnimations.CloseMenu();
        }

        public void ShowWinPanel(int collectedCoins, int timeBonus, float modifier, int totalCoins)
        {
            winPanel.gameObject.SetActive(true);
            winPanel.SetTexts(collectedCoins, timeBonus, modifier, totalCoins);
        }

        public void ShowLosePanel(int collectedCoins, int penalty, int totalCoins)
        {
            losePanel.gameObject.SetActive(true);
            losePanel.SetTexts(collectedCoins, penalty, totalCoins);
        }
    }
}
