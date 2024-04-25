using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuUIController : MonoBehaviour
    {
        [Header("Start Screen")]
        [SerializeField] private List<GameObject> startScreenElements;
        
        [Space(10)]
        [Header("Garage")] 
        [SerializeField] private List<GameObject> garageElements;

        public void ShowGarageElements()
        {
            foreach (var element in startScreenElements)
                element.SetActive(false);
            
            foreach (var element in garageElements)
                element.SetActive(true);
        }
        
        public void HideGarageElements()
        {
            foreach (var element in startScreenElements)
                element.SetActive(true);
            
            foreach (var element in garageElements)
                element.SetActive(false);
        }
    }
}
