using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityTargetting : MonoBehaviour
{
    public UnityEvent abilityTargetAct;

    private void OnMouseDown()
    {
        abilityTargetAct?.Invoke();
    }
}
