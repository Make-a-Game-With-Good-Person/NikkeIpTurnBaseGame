using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class SkillViewSideToSideState : ICameraState
{
    public float offsetZDistance = 1f;
    public float offsetXDistance = 5f;
    public float height = 1.5f; // ī�޶��� ����
    public float moveSpeed = 0.3f; // ī�޶��� �¿� �̵� �ӵ�
    Vector3 forward;
    Vector3 right;
    Vector3 targetPos;
    Vector3 camStartPos;

    float lerpTime = 0f; // ���� �����


    void SetCamInit(CameraStateController camera)
    {
        forward = camera.target.forward.normalized;//(0,0,1)
        right = camera.target.right.normalized; // (1,0,0)

        targetPos = camera.target.position
                                 + (forward * offsetZDistance) // ĳ���� ���鿡�� ���� �Ÿ�
                                 - (right * offsetXDistance); // �¿� �̵�
        targetPos.y = camera.target.position.y + height; // ���� ����

        Debug.Log(targetPos);
        camStartPos = camera.target.position
                                 + (forward * offsetZDistance) // ĳ���� ���鿡�� ���� �Ÿ�
                                 + (right * offsetXDistance); // �¿� �̵�
        camStartPos.y = camera.target.position.y + height; // ���� ����
        Debug.Log(camStartPos);

        Vector3 lookDirection = -camera.target.forward; // ĳ������ �� �� ����
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        camera.transform.rotation = targetRotation;

        camera.camStart = false;
    }


    public void UpdateState(CameraStateController camera)
    {
        if (camera.camStart)
        {
            SetCamInit(camera);
        }
        if (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime * moveSpeed; // ���� ����� ������Ʈ
            camera.transform.position = Vector3.Lerp(camStartPos, targetPos, lerpTime);
        }
        
    }
}
