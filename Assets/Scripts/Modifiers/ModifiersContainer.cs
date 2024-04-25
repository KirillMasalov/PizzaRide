using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Modifiers
{
    public class ModifiersContainer : MonoBehaviour
    {
        public static ModifiersContainer Instance { get; private set; }
        [SerializeField] private List<Modifier> modifiers;
        
        public Dictionary<string, Modifier> RegisteredModifiers { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            RegisteredModifiers = new Dictionary<String, Modifier>();
            foreach (var m in modifiers)
            {
                RegisteredModifiers[m.Name] = m;
            }
        }

        public void ActivateModifier(String modifierName)
        {
            ValidateModifierType(modifierName);
            RegisteredModifiers[modifierName].ModifierOn();
        }
        
        public void DeactivateModifier(String modifierName)
        {
            ValidateModifierType(modifierName);
            RegisteredModifiers[modifierName].ModifierOff();
        }

        private void ValidateModifierType(String modifierName)
        {
            if (!RegisteredModifiers.ContainsKey(modifierName))
            {
                throw new KeyNotFoundException($"ModifierContainer | {modifierName} not registered in container");
            }
        }
    }
}