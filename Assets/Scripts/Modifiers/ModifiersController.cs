using System;
using System.Collections;
using System.Collections.Generic;
using Ride;
using UnityEngine;
using UnityEngine.Events;

namespace Modifiers
{
    public class ModifiersController : MonoBehaviour
    {
        public static ModifiersController Instance { get; private set; }
        
        [SerializeField] private float maxValue;
        
        public HashSet<ModifierElement> ActiveModifiers { get; private set; } = new HashSet<ModifierElement>();
        public float CurrentValue { get; private set; }
        public UnityEvent modifiersApplied;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CurrentValue = 1f;
            RideController.Instance.RideTimer.tick.AddListener(ApplyModifiers);
        }

        private void ApplyModifiers()
        {
            var removeModifiers = new List<ModifierElement>();
            foreach (var modifier in ActiveModifiers)
            {
                CurrentValue += modifier.Value;
                CurrentValue = Mathf.Clamp(CurrentValue, 0, maxValue);
                
                if (modifier.MType == ModifierType.Disposable)
                {
                    removeModifiers.Add(modifier);
                }
            }
            
            modifiersApplied?.Invoke();
            
            foreach (var modifier in removeModifiers)
                ActiveModifiers.Remove(modifier);
        }
    }
}
