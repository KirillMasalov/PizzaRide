using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Modifiers
{
    [CreateAssetMenu(fileName = "Disposable", menuName = "Modifier/Disposable", order = 51)]
    public class DisposableModifier : Modifier
    {
        public override ModifierType MType => ModifierType.Disposable;

        public override void ModifierOn()
        {
            ModifiersController.Instance.ActiveModifiers.Add(new ModifierElement(Name, MType, Value));
        }

        public override void ModifierOff() { }
    }
}
