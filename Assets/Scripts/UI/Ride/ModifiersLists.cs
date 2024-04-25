using System;
using System.Collections;
using System.Collections.Generic;
using Modifiers;
using TMPro;
using UnityEngine;

namespace UI.Ride
{
    public class ModifiersLists : MonoBehaviour
    {
        [SerializeField] private RectTransform positiveList;
        [SerializeField] private RectTransform negativeList;

        [SerializeField] private GameObject positivePrefab;
        [SerializeField] private GameObject negativePrefab;

        private Dictionary<string, GameObject> modifiersAttributes;
        private HashSet<string> drawedAttributes;
        private HashSet<string> disposableAttributesBuffer;

        private void Awake()
        {
            if (positivePrefab is null || negativePrefab is null)
                throw new NullReferenceException("ModifiersList | Prefab is null");

            modifiersAttributes = new Dictionary<string, GameObject>();
            drawedAttributes = new HashSet<string>();
            disposableAttributesBuffer = new HashSet<string>();
            
            ModifiersController.Instance.modifiersApplied.AddListener(Redraw);
        }

        private void Start()
        {
            foreach (var m in ModifiersContainer.Instance.RegisteredModifiers.Values)
            {
                GameObject newAttributePanel;
                
                if (m.Value >= 0)
                    newAttributePanel = Instantiate(positivePrefab, parent:positiveList);
                else
                    newAttributePanel = Instantiate(negativePrefab, parent:negativeList);

                newAttributePanel.GetComponent<TextMeshProUGUI>().text = m.Description;
                modifiersAttributes[m.Name] = newAttributePanel;
                newAttributePanel.SetActive(false);
            }
        }

        private void Redraw()
        {
            var newAttributes = new HashSet<string>();
            foreach (var m in ModifiersController.Instance.ActiveModifiers)
            {
                newAttributes.Add(m.MName);
                modifiersAttributes[m.MName].SetActive(true);
            }
            
            RemoveDisposableAttrFromBuffer(newAttributes);
            
            foreach (var oldAttr in drawedAttributes)
            {
                if (!newAttributes.Contains(oldAttr))
                {
                    if (ModifiersContainer.Instance.RegisteredModifiers[oldAttr].MType == ModifierType.Disposable)
                        disposableAttributesBuffer.Add(oldAttr);
                    else
                        modifiersAttributes[oldAttr].SetActive(false);
                }
            }

            drawedAttributes = newAttributes;
        }

        private void RemoveDisposableAttrFromBuffer(HashSet<string> newAttributes)
        {
            var removeDisposableAttr = new List<string>();
            foreach (var attr in disposableAttributesBuffer)
            {
                if (!newAttributes.Contains(attr))
                    modifiersAttributes[attr].SetActive(false);
                else
                    removeDisposableAttr.Add(attr);
            }
            foreach (var attr in removeDisposableAttr)
                disposableAttributesBuffer.Remove(attr);
        }
    }
}
