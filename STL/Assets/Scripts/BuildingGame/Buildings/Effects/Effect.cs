using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect: ScriptableObject
{
    public abstract void TriggerEffect();

    public abstract string RetrieveEffectToolTip();
}
