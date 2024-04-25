using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class EmptyBoxChecker : MonoBehaviour
    {
        private const float CHECK_RADIUS = 3f;

        public bool Check(Vector3 pos)
        {
            var colliders = Physics.OverlapSphere(pos, CHECK_RADIUS);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("Car"))
                    return false;
            }

            return true;
        }

    }
}
