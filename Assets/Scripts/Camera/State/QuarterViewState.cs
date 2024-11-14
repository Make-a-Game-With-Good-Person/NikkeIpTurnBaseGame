using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterViewState : ICameraState
{
    public void UpdateState(CameraController CC)
    {
        /*Vector3 targetPosition = CC.target.position + CC.quarterViewoffset;
        CC.transform.position = Vector3.Lerp(CC.transform.position, targetPosition, Time.deltaTime * CC.transitionSpeed);
        CC.transform.LookAt(CC.target);*/

        /*// 위치 offset 적용
        Vector3 desiredPosition = CC.target.position + CC.quarterViewoffset;
        CC.transform.position = Vector3.Lerp(CC.transform.position, desiredPosition, Time.deltaTime * CC.transitionSpeed);

        // 회전 offset 적용
        Quaternion desiredRotation = Quaternion.Euler(CC.quarterViewrotationOffset) * Quaternion.LookRotation(CC.target.position - CC.transform.position);
        CC.transform.rotation = Quaternion.Slerp(CC.transform.rotation, desiredRotation, Time.deltaTime * CC.transitionSpeed);*/
    }
}
