using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(menuName = "MotorcycleData")]
    public class MotorcycleData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float BreakDeceleration { get; private set; }
        
        [field: SerializeField] public float RotateAcceleration { get; private set; }
        [field: SerializeField] public float MaxRotateSpeed { get; private set; }
        [field: SerializeField] public float MaxSlantAngle { get; private set; }
        public string GetMaxCoinsSaveString => $"{Name}_Max_Coins";
        public string GetMaxModifierSaveString => $"{Name}_Max_Modifier";
        public string GetBestTimeSaveString =>$"{Name}_Best_Time";
        
    }
}