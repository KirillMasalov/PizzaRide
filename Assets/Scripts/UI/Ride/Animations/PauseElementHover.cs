using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Ride.Animations
{
    public class PauseElementHover : MonoBehaviour
    {
        private const string ENTRY_BOOL = "Entry";
        private const string EXIT_BOOL = "Exit";
        
        [SerializeField] private Animator animator;

        public void PointerEnter()
        {
            animator.SetBool(ENTRY_BOOL, true);
            animator.SetBool(EXIT_BOOL, false);
        }

        public void PointerExit()
        {
            animator.SetBool(ENTRY_BOOL, false);
            animator.SetBool(EXIT_BOOL, true);
        }
    }
}
