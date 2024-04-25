using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modifiers.ModifiersTriggers
{
    public class OppositeLine : MonoBehaviour
    {
        private const string MODIFIER_NAME = "OppositeLine";
        
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
                ModifiersContainer.Instance.ActivateModifier(MODIFIER_NAME);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
                ModifiersContainer.Instance.DeactivateModifier(MODIFIER_NAME);
        }
    }
}