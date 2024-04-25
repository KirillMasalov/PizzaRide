using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Unity.VisualScripting;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private SettingsController settingsController;

        private void Start()
        {
            settingsController.Initialize();
        }
    }
}