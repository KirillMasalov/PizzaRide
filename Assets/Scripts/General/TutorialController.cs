using System;
using System.Collections;
using System.Collections.Generic;
using Ride;
using UnityEngine;

namespace General
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tutorialPanels;
        private int currentPanelIndex;

        private void Start()
        {
            if (!SavesManager.Instance.WasTutorial())
            {
                RideController.Instance.StartTutorial();
                tutorialPanels[0].SetActive(true);
            }
        }

        public void NextPanel()
        {
            tutorialPanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;
            if(currentPanelIndex < tutorialPanels.Count)
                tutorialPanels[currentPanelIndex].SetActive(true);
            else
            {
                RideController.Instance.EndTutorial();
                SavesManager.Instance.SetWasTutorial();
            }
        }
    }
}