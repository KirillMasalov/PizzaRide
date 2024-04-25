using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotorCycle
{
    public class MotorcycleColorRenderer : MonoBehaviour
    {
        private const string COLOR_KEY = "_Color";
        [SerializeField] private List<MeshRenderer> colorsObjects;

        public void SetColor(Color color)
        {
            foreach (var co in colorsObjects)
                co.sharedMaterial.SetColor(COLOR_KEY, color);
        }
        
        
    }
}