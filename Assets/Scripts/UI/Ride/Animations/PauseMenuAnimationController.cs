using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Ride.Animations
{
    public class PauseMenuAnimationController : MonoBehaviour
    {
        private const string OPEN_TRIGGER = "Open";
        private const string CLOSE_TRIGGER = "Close";
        
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject pauseButton;

        public void OpenMenu()
        {
            pauseButton.SetActive(false);
            animator.SetTrigger(OPEN_TRIGGER);
        }

        public void CloseMenu()
        {
            pauseButton.SetActive(true);
            animator.SetTrigger(CLOSE_TRIGGER);
        }
    }
}
