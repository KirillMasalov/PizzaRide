using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modifiers
{
    [CreateAssetMenu(fileName = "Permanent", menuName = "Modifier/Permanent", order = 51)]
    public class PermanentModifier : Modifier
    {
        public override ModifierType MType => ModifierType.Permanent;
        private ModifierElement element;

        public override void ModifierOn()
        {
            element ??= new ModifierElement(Name, MType, Value);
            ModifiersController.Instance.ActiveModifiers.Add(element);
        }

        public override void ModifierOff()
        {
            ModifiersController.Instance.ActiveModifiers.Remove(element);
        }
    }
}