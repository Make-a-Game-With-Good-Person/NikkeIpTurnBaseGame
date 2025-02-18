using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
    public float seconds;

    private void OnEnable()
    {
        StartCoroutine(DelayDisable());
    }

    IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(seconds);

        this.gameObject.SetActive(false);
    }
}
