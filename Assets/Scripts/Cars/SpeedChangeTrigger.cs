using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class SpeedChangeTrigger : MonoBehaviour
    {
        [SerializeField] private Car currentCar;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                currentCar.Stop();
                currentCar.DecreaseSpeed();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
                currentCar.Go();
        }
    }
}
