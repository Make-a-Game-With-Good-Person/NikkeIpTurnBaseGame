using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterViewState : ICameraState
{
    public void UpdateState(CameraController CC)
    {
        Vector3 targetPosition = CC.target.position + CC.quarterViewoffset;
        CC.transform.position = Vector3.Lerp(CC.transform.position, targetPosition, Time.deltaTime * CC.transitionSpeed);
        CC.transform.rotation = Quaternion.Lerp(CC.transform.rotation, Quaternion.Euler(CC.quarterViewrotationOffset), Time.deltaTime * CC.rotationSpeed);
    }
}
