using System;
using System.Collections;
using System.Collections.Generic;
using MotorCycle;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace Rider
{
    public class RiderConstraintsController : MonoBehaviour
    {
        [SerializeField] private RigBuilder builder;
        [SerializeField] private RagDollController ragDollController;
        
        [Space(10)]
        [Header("Constraints")] 
        [SerializeField] private Transform hips;
        [SerializeField] private TwoBoneIKConstraint rightLeg;
        [SerializeField] private TwoBoneIKConstraint leftLeg;
        [SerializeField] private TwoBoneIKConstraint rightHand;
        [SerializeField] private TwoBoneIKConstraint leftHand;
        public void Initialize(MotorcycleConstraintsController mcConstraints)
        {
            transform.parent = mcConstraints.transform;
            transform.localEulerAngles = new Vector3(mcConstraints.RiderXAngle, transform.localEulerAngles.y,
                transform.localEulerAngles.z);
            
            hips.transform.position = mcConstraints.HipsTarget.position;
            
            rightLeg.data.target = mcConstraints.RightLegTarget;
            rightLeg.data.hint = mcConstraints.RightLegHint;
            
            leftLeg.data.target = mcConstraints.LeftLegTarget;
            leftLeg.data.hint = mcConstraints.LeftLegHint;
            
            rightHand.data.target = mcConstraints.RightHandTarget;
            rightHand.data.hint = mcConstraints.RightHandHint;
            
            leftHand.data.target = mcConstraints.LeftHandTarget;
            leftHand.data.hint = mcConstraints.LeftHandHint;
            
            builder.Build();
        }

        public void Release()
        {
            transform.parent = null;
            
            rightLeg.data.target = null;
            rightLeg.data.hint = null;
            
            leftLeg.data.target = null;
            leftLeg.data.hint = null;
            
            rightHand.data.target = null;
            rightHand.data.hint = null;
            
            leftHand.data.target = null;
            leftHand.data.hint = null;
            
            builder.Build();
            
            ragDollController.RagDollOn();
        }
    }
}
