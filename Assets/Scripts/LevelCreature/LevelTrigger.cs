using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreature
{

    public class LevelTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LevelController.Instance.AddLevelPart();
                Destroy(gameObject);
            }
        }
    }
}
