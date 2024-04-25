using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class CarSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public CarMovementNode StartTargetNode { get; set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
