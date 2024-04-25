using System;
using System.Collections;
using System.Collections.Generic;
using LevelCreature;
using UnityEngine;
using UnityEngine.Serialization;

namespace MotorCycle
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSmooth;
        [SerializeField] private Transform moveTarget;
        
        [Space(10)]
        [Header("YRotation")] 
        [SerializeField] [Range(0f, 4f)] private float yRotationRatio;
        [SerializeField] [Range(0, 90f)] private float maxYRotation;
        [SerializeField] [Range(0, 1f)] private float yRotationReturnRatio;

        [Space(10)] 
        [Header("ZRotation")]
        [SerializeField] [Range(0, 1f)] private float zRotationRatio;

        private float zRotation;
        private float yRotation;
        private Transform tr;

        private void Awake()
        {
            tr = transform;
        }

        public void RotateByZ(float value)
        {
            zRotation = value * zRotationRatio;
        }

        public void SetRotationByY(float value)
        {
            yRotation += value * yRotationRatio;
            yRotation = Mathf.Clamp(yRotation, -maxYRotation, maxYRotation);
        }

        public void Update()
        {
            yRotation *= yRotationReturnRatio;
        }

        private void FixedUpdate()
        {
            var currentRot = tr.localEulerAngles;
            tr.localEulerAngles = new Vector3(currentRot.x, moveTarget.eulerAngles.y, zRotation);
            tr.position = Vector3.Lerp(tr.position, moveTarget.position, moveSmooth);
        }
    }
}
