using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class ShoulderViewState : ICameraState
{
    public void UpdateState(CameraController CC)
    {
        /*Vector3 targetPosition = CC.target.position + CC.shoulderViewOffset;
        CC.transform.position = Vector3.Lerp(CC.transform.position, targetPosition, Time.deltaTime * CC.transitionSpeed);
        CC.transform.LookAt(CC.target);*/

        /*// ��ġ offset ����
        Vector3 desiredPosition = CC.target.position + CC.shoulderViewOffset;
        CC.transform.position = Vector3.Lerp(CC.transform.position, desiredPosition, Time.deltaTime * CC.transitionSpeed);

        // ȸ�� offset ����
        Quaternion desiredRotation = Quaternion.Euler(CC.shoulderViewrotationOffset) * Quaternion.LookRotation(CC.target.position - CC.transform.position);
        CC.transform.rotation = Quaternion.Slerp(CC.transform.rotation, desiredRotation, Time.deltaTime * CC.transitionSpeed);*/

    }
}
