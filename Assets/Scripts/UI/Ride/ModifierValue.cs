using System;
using System.Collections;
using System.Collections.Generic;
using Modifiers;
using TMPro;
using UnityEngine;

namespace UI.Ride
{
    public class ModifierValue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI modifierText;
        [SerializeField] private TextMeshProUGUI sign;
        
        [SerializeField] private Color increaseColor;
        [SerializeField] private Color neutralColor;
        [SerializeField] private Color decreaseColor;

        private float previousValue = 1f;
        private void Start()
        {
            ModifiersController.Instance.modifiersApplied.AddListener(RedrawModifiersValue);
        }

        private void RedrawModifiersValue()
        {
            var newValue = ModifiersController.Instance.CurrentValue;
            modifierText.text = newValue.ToString("0.000");
            ChangeColor(newValue - previousValue);
            previousValue = newValue;
        }

        private void ChangeColor(float diff)
        {
            if (diff > 0)
            {
                sign.color = increaseColor;
                modifierText.color = increaseColor;
            }

            if (diff < 0)
            {
                sign.color = decreaseColor;
                modifierText.color = decreaseColor;
            }

            if (diff == 0)
            {
                sign.color = neutralColor;
                modifierText.color = neutralColor;
            }
        }
    }
}
