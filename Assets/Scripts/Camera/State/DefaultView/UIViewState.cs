using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewState : IUICameraState
{
    public void UpdateState(SquadUICamController CC)
    {
        Camera.main.orthographic = true;
        Vector3 targetPosition = CC.defaultTarget.position;
        CC.transform.position = Vector3.Lerp(CC.transform.position, targetPosition, Time.deltaTime * CC.transitionSpeed);
        CC.transform.rotation = Quaternion.Lerp(CC.transform.rotation, Quaternion.Euler(CC.defaultViewrotationOffset), Time.deltaTime * CC.rotationSpeed);
    }
}
