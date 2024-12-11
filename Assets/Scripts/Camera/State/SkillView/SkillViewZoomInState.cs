using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillViewZoomInState : ICameraState
{
    float targetDistance = 2.5f;    // ��ǥ �Ÿ�
    float currentDistance = 10f;
    float currentSpeed = 0f;      // ���� �ӵ�
    float zoomSpeed = 4f;         // ���ӵ�

    
    public void UpdateState(CameraStateController camera)
    {
        if (!camera.camStart || currentDistance <= targetDistance)
        {
            return;
        }

        currentSpeed += zoomSpeed * Time.deltaTime;
        currentDistance -= currentSpeed * Time.deltaTime;

        Vector3 dir = (camera.target.position - camera.transform.position).normalized;
        camera.transform.position = camera.target.position - dir * currentDistance;

        camera.transform.LookAt(camera.target);

        if (currentDistance <= targetDistance)
        {
            currentDistance = targetDistance;
            camera.camStart = false;
        }
    }

}
