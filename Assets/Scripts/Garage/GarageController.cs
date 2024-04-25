using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General;
using MotorCycle;
using UnityEngine;
using UnityEngine.Events;

namespace Garage
{
    public class GarageController : MonoBehaviour
    {
        private static IReadOnlyList<MotorcycleDataContainer> globalMotorcyclesInShop;
        
        [SerializeField] private List<MotorcycleDataContainer> motorcyclesInShop;
        [SerializeField] private FlexibleColorPicker colorPicker;
        
        private int currentMotorcycleIndex;
        private Dictionary<int,MotorcycleColorRenderer>  indexToColorRenderer;

        public UnityEvent<MotorcycleData> changeCurrentMotorcycle;
        public UnityEvent openGarage;
        public UnityEvent buyMotorcycle;

        private void Awake()
        {
            indexToColorRenderer = new Dictionary<int, MotorcycleColorRenderer>();
            globalMotorcyclesInShop = motorcyclesInShop;
            colorPicker.onColorChange.AddListener(ChangeColor);
        }

        public void NextMotorcycle()
        {
            currentMotorcycleIndex++;
            if (currentMotorcycleIndex >= motorcyclesInShop.Count)
                currentMotorcycleIndex = 0;

            CacheColorRenderer(currentMotorcycleIndex);
            changeCurrentMotorcycle?.Invoke(motorcyclesInShop[currentMotorcycleIndex].Data);
            ShowCurrentMotorcycle();
        }
        
        public void PrevMotorcycle()
        {
            currentMotorcycleIndex--;
            if (currentMotorcycleIndex < 0)
                currentMotorcycleIndex = motorcyclesInShop.Count - 1;
            
            CacheColorRenderer(currentMotorcycleIndex);
            changeCurrentMotorcycle?.Invoke(motorcyclesInShop[currentMotorcycleIndex].Data);
            ShowCurrentMotorcycle();
        }

        public void OpenGarage()
        {
            var currentMotoIndex = motorcyclesInShop.FindIndex(dc => dc.Data == MotoManagementController.Instance.CurrentMotorcycle);
            currentMotorcycleIndex = currentMotoIndex;
            
            CacheColorRenderer(currentMotoIndex);
            openGarage?.Invoke();
            changeCurrentMotorcycle?.Invoke(motorcyclesInShop[currentMotoIndex].Data);
            ShowCurrentMotorcycle();
        }

        private void ShowCurrentMotorcycle()
        {
            for (var i = 0; i < motorcyclesInShop.Count; i++)
            {
                motorcyclesInShop[i].gameObject.SetActive(i == currentMotorcycleIndex);
            }
        }

        private void CacheColorRenderer(int index)
        {
            if (!indexToColorRenderer.ContainsKey(index))
                indexToColorRenderer[index] = motorcyclesInShop[index].GetComponent<MotorcycleColorRenderer>();
        }

        public void ChooseCurrentMotorcycle()
        {
            var currentMotorcycle = motorcyclesInShop[currentMotorcycleIndex];
            MotoManagementController.Instance.SetCurrentMotorcycle(currentMotorcycle.Data);
            changeCurrentMotorcycle?.Invoke(currentMotorcycle.Data);
        }

        public void BuyCurrentMotorcycle()
        {
            var currentMotorcycle = motorcyclesInShop[currentMotorcycleIndex];
            var success = MotoManagementController.Instance.BuyMotorcycle(currentMotorcycle.Data);
            if(success)
                buyMotorcycle?.Invoke();
        }

        private void ChangeColor(Color color)
        {
            if (indexToColorRenderer.ContainsKey(currentMotorcycleIndex))
            {
                indexToColorRenderer[currentMotorcycleIndex].SetColor(color);
            }
        }

        public static MotorcycleData GetMotoDataByLowerName(string motorcycleName)
        {
            return globalMotorcyclesInShop.FirstOrDefault(m => m.Data.Name.ToLower() == motorcycleName).Data;
        }
    }
}