using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuCameraMovement : MonoBehaviour
    {
        [Space(10)] 
        [Header("Settings")]
        [SerializeField] private float positionSmooth;
        [SerializeField] private float rotationSmooth;
        [SerializeField] private float positionEps;
        [SerializeField] private float rotationEps;

        [Space(10)] 
        [Header("Default state")]
        [SerializeField] private Vector3 defaultPos;
        [SerializeField] private Vector3 defaultRotation;
        
        [Space(10)]
        [Header("Garage state")]
        [SerializeField] private Vector3 garagePos;
        [SerializeField] private Vector3 garageRotation;
        
        private Vector3? targetPos;
        private Vector3? targetRotation;
        
        private Transform cameraTransform;

        private void Awake()
        {
            cameraTransform = transform;
            cameraTransform.position = defaultPos;
            cameraTransform.localEulerAngles = defaultRotation;
        }
        
        public void SetDefaultState()
        {
            SetTarget(defaultPos, defaultRotation);
        }

        public void SetGarageState()
        {
            SetTarget(garagePos, garageRotation);
        }

        private void SetTarget(Vector3 position, Vector3 rotation)
        {
            targetPos = position;
            targetRotation = new Vector3(rotation.x, rotation.y, rotation.z);
        }

        private void FixedUpdate()
        {
            ChangePosition();
            ChangeRotation();
        }

        private void ChangePosition()
        {
            if (targetPos.HasValue)
            {
                if (Vector3.Magnitude(cameraTransform.position - targetPos.Value) < positionEps)
                {
                    cameraTransform.position = targetPos.Value;
                    targetPos = null;
                }
                else
                    cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPos.Value, positionSmooth);

            }
        }
        
        private void ChangeRotation()
        {
            if (targetRotation.HasValue)
            {
                if (Vector3.Magnitude(cameraTransform.localEulerAngles - targetRotation.Value) < rotationEps)
                {
                    cameraTransform.rotation = Quaternion.Euler(targetRotation.Value);
                    targetRotation = null;
                }
                else
                {
                    cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation,
                        Quaternion.Euler(targetRotation.Value), rotationSmooth);
                }
            }
        }
    }
}
