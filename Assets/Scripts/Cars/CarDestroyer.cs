using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class CarDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
