using System;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Buttons
{
    public class StartButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(GameController.Instance.StartRide);
        }

        private void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}