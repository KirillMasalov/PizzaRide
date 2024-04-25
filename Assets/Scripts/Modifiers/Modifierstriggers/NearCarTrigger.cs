using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Modifiers.ModifiersTriggers
{
    public class NearCarTrigger : MonoBehaviour
    {
        private const string MODIFIER_NAME = "NearCar";
        private bool isUsed = false;
        private void OnTriggerEnter(Collider other)
        {
            if (!isUsed && other.gameObject.CompareTag("Player"))
            {
                isUsed = true;
                ModifiersContainer.Instance.ActivateModifier(MODIFIER_NAME);
            }
        }
    }
}
