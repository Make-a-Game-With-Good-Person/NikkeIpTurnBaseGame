using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIAnimationEvent : MonoBehaviour
{
    public UnityEvent animEnd;
    public void OnAnimEnd()
    {
        animEnd?.Invoke();
    }
}
