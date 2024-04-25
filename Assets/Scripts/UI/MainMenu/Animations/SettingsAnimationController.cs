using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MainMenu.Animations
{
    public class SettingsAnimationController : MonoBehaviour
    {
        private const string OPEN_KEY = "Open";
        private const string CLOSE_KEY = "Close";

        [SerializeField] private Animator animator;
        [SerializeField] private GameObject mainMenuButtonsContainer;
        [SerializeField] private GameObject mainMenuBg;

        public void Open()
        {
            mainMenuButtonsContainer.SetActive(false);
            mainMenuBg.SetActive(false);
            animator.SetTrigger(OPEN_KEY);
        }

        public void Close()
        {
            mainMenuButtonsContainer.SetActive(true);
            mainMenuBg.SetActive(true);
            animator.SetTrigger(CLOSE_KEY);
        }
    }
}
