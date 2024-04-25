using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotorCycle
{
    public class MotoAudioController : MonoBehaviour
    {
       [SerializeField] private MotorSound motorSound;
       private MotorcycleMovementController motorcycleMovement;
       
       private void Start()
       {
           var currentTransform = transform;
           while (motorcycleMovement is null)
           {
               motorcycleMovement = currentTransform.GetComponent<MotorcycleMovementController>();
               if(currentTransform.parent is null)
                   break;

               currentTransform = currentTransform.parent;
           }
            
           if (motorcycleMovement is null)
               throw new NullReferenceException("MotoAudioController | Can't find MotorcycleMovementController parent");

           motorSound.Initialize(motorcycleMovement);
           
       }
    }
}