using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Cars
{
    public class Car : MonoBehaviour
    {
        private const float CHANGE_TARGET_EPS = 3f;
        private const float MOVE_SMOOTH = 0.4f;
        private const float SPEED_DECREASE_RATIO = 0.95f;
        private const float ROTATION_SMOOTH = 0.2f;

        [SerializeField] [Min(0)] private float minMoveSpeed;
        [SerializeField] [Min(0)] private float maxMoveSpeed;
        [SerializeField] private Rigidbody rigidBody;

        private CarMovementNode targetNode;
        private Transform tr;
        private Vector3 targetPos;
        private float moveSpeed;
        private float currentSpeed;
        private Quaternion? targetRotation;

        public void Initialize(CarMovementNode initialTarget)
        {
            tr = transform;
            moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            currentSpeed = moveSpeed;
            targetNode = initialTarget;
            targetPos = new Vector3(targetNode.transform.position.x, 
                transform.position.y, targetNode.transform.position.z);
        }

        public void Stop()
        {
            currentSpeed = 0;
        }

        public void Go()
        {
            currentSpeed = moveSpeed;
        }
        
        public void DecreaseSpeed()
        {
            moveSpeed *= SPEED_DECREASE_RATIO;
        }


        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            if (targetNode != null)
            {
                var currentPos = tr.position;
                rigidBody.velocity = (targetPos - currentPos).normalized * (currentSpeed * MOVE_SMOOTH);
                if (Mathf.Pow(currentPos.x - targetPos.x, 2) 
                    + Mathf.Pow(currentPos.z - targetPos.z, 2) < CHANGE_TARGET_EPS)
                    ChangeTargetNode();
            }
        }

        private void Rotate()
        {
            if (targetRotation.HasValue)
            {
                tr.rotation = Quaternion.Lerp(tr.rotation, targetRotation.Value, ROTATION_SMOOTH);
            }
        }

        private void ChangeTargetNode()
        {
            if (targetNode.NextNode != null && targetNode != null)
            {
                targetNode = targetNode.NextNode;
                var currentPos = tr.position;

                var targetNodeTransform = targetNode?.transform;
                if (targetNodeTransform is not null)
                {
                    var nodePosition = targetNode.transform.position;
                    targetPos = new Vector3(nodePosition.x, currentPos.y, nodePosition.z);
                    var rotation = Quaternion.LookRotation(targetPos - currentPos);
                    targetRotation = rotation;
                }
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        }
    }
}
