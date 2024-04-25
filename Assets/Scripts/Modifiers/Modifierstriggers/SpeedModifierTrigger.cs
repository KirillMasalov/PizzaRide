using System;
using System.Collections;
using System.Collections.Generic;
using MotorCycle;
using UnityEngine;

namespace Modifiers.ModifiersTriggers
{
    public class SpeedModifierTrigger : MonoBehaviour
    {
        private const string MODIFIER_NAME = "HighSpeed";
        [SerializeField] private MotorcycleMovementController movement;
        [SerializeField] private float speedThreshold;
        private void Update()
        {
            if (movement.CurrentSpeed > speedThreshold)
                ModifiersContainer.Instance.ActivateModifier(MODIFIER_NAME);
            else
                ModifiersContainer.Instance.DeactivateModifier(MODIFIER_NAME);
        }
    }
}