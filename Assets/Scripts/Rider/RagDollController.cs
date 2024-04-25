using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rider
{
    public class RagDollController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float upForceRatio;
        [SerializeField] private float hitForceRatio;
        
        [Space(10)]
        [Header("Links")]
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody body;
        [SerializeField] private List<Rigidbody> dollParts;
        [SerializeField] private List<Collider> colliders;

        public void RagDollOn()
        {
            animator.enabled = false;
            foreach (var part in dollParts)
            {
                part.isKinematic = false;
            }

            foreach (var col in colliders)
            {
                col.enabled = true;
            }
        }

        public void AddImpulseForce(Vector3 direction)
        {
            body.AddForce(direction * hitForceRatio + Vector3.up * upForceRatio, ForceMode.Impulse);
        }
    }
}
