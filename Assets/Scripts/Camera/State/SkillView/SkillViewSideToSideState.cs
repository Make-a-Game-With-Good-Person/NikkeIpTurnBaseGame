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
    public float height = 1.5f; // 카메라의 높이
    public float moveSpeed = 0.3f; // 카메라의 좌우 이동 속도
    Vector3 forward;
    Vector3 right;
    Vector3 targetPos;
    Vector3 camStartPos;

    float lerpTime = 0f; // 누적 진행률


    void SetCamInit(CameraStateController camera)
    {
        forward = camera.target.forward.normalized;//(0,0,1)
        right = camera.target.right.normalized; // (1,0,0)

        targetPos = camera.target.position
                                 + (forward * offsetZDistance) // 캐릭터 정면에서 일정 거리
                                 - (right * offsetXDistance); // 좌우 이동
        targetPos.y = camera.target.position.y + height; // 높이 조정

        Debug.Log(targetPos);
        camStartPos = camera.target.position
                                 + (forward * offsetZDistance) // 캐릭터 정면에서 일정 거리
                                 + (right * offsetXDistance); // 좌우 이동
        camStartPos.y = camera.target.position.y + height; // 높이 조정
        Debug.Log(camStartPos);

        Vector3 lookDirection = -camera.target.forward; // 캐릭터의 등 뒤 방향
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
            lerpTime += Time.deltaTime * moveSpeed; // 누적 진행률 업데이트
            camera.transform.position = Vector3.Lerp(camStartPos, targetPos, lerpTime);
        }
        
    }
}
