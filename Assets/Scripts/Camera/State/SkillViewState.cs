using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SkillViewState : ICameraState
{
    float upRotationSpeed = 120f; // 카메라 회전 속도
    float downRotationSpeed = 240f;
    float descendSpeed = 4f; // 카메라 하강 속도
    float finalHeight = 1f; // 연출 종료 시 카메라의 높이

    float upTargetAngle = 280f; // 카메라가 멈출 목표 각도 (플레이어 정면 기준)
    float downTargetAngle = 520f;
    
    private float currentAngle = 0f; // 현재 회전 각도 추적
    float angleStep;

    public void UpdateState(CameraStateController camera)
    {
        // 현재 높이가 목표 높이보다 크면 계속 하강
        if (camera.currentHeight > finalHeight)
        {
            camera.currentHeight -= descendSpeed * Time.deltaTime; // 점진적 하강
            if (camera.currentHeight < finalHeight)
                camera.currentHeight = finalHeight; // 최저 높이 제한
        }

        

        if (camera.skillCamDir == 1) // 방향 조절
        {
            // 회전이 목표 각도를 초과했으면 멈춤
            if (currentAngle >= upTargetAngle)
            {
                camera.transform.LookAt(camera.target); // 멈출 때도 캐릭터를 응시
                return; // 회전 중지
            }

            angleStep = upRotationSpeed * Time.deltaTime;

            // 카메라가 target을 기준으로 회전
            camera.transform.RotateAround(
                camera.target.position,        // 회전 중심 (캐릭터의 위치)
                Vector3.up,             // 회전 축 (Y축 기준으로 회전)
                angleStep
            );
        }
        else if (camera.skillCamDir == 2)
        {
            // 회전이 목표 각도를 초과했으면 멈춤
            if (currentAngle >= downTargetAngle)
            {
                camera.transform.LookAt(camera.target); // 멈출 때도 캐릭터를 응시
                return; // 회전 중지
            }

            angleStep = downRotationSpeed * Time.deltaTime;

            // 카메라가 target을 기준으로 회전
            camera.transform.RotateAround(
                camera.target.position,        // 회전 중심 (캐릭터의 위치)
                Vector3.down,             // 회전 축 (Y축 기준으로 회전)
                angleStep
            );
        }

        // 현재 각도 누적
        currentAngle += angleStep;


        // 회전 후, 카메라 높이를 현재 높이에 맞게 조정
        Vector3 currentPosition = camera.transform.position;
        currentPosition.y = camera.currentHeight; // 높이 갱신
        camera.transform.position = currentPosition;

        // 항상 캐릭터를 바라보게 설정
        camera.transform.LookAt(camera.target);
    }
}
