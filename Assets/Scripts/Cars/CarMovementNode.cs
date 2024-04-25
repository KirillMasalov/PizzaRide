using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars
{
    public class CarMovementNode : MonoBehaviour
    {
        [field: SerializeField] public CarMovementNode NextNode { get; set; }

        private void OnDrawGizmos()
        {
            if (NextNode is not null)
            {
                Gizmos.color = Color.green;
                var currentPos = transform.position;
                Gizmos.DrawLine(currentPos, NextNode.transform.position);
                Gizmos.DrawSphere(currentPos, 0.3f);
            }
        }
    }
}
