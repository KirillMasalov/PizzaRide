using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modifiers.ModifiersTriggers
{
    public class OffRoadTrigger : MonoBehaviour
    {
        private const string MODIFIER_NAME = "OffRoad";
        
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
