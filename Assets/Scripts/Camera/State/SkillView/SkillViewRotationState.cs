using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SkillViewRotationState : ICameraState
{
    float upRotationSpeed = 180f; // ī�޶� ȸ�� �ӵ�
    float descendSpeed = 4.5f; // ī�޶� �ϰ� �ӵ�
    float finalHeight = 0.5f; // ���� ���� �� ī�޶��� ����
    float currentHeight = 10f; // ���� ī�޶� ����
    float upTargetAngle = 330f; // ī�޶� ���� ��ǥ ���� (�÷��̾� ���� ����)

    private float currentAngle = 0f; // ���� ȸ�� ���� ����
    float angleStep;

    public void UpdateState(CameraStateController camera)
    {
        // ���� ���̰� ��ǥ ���̺��� ũ�� ��� �ϰ�
        if (currentHeight > finalHeight)
        {
            currentHeight -= descendSpeed * Time.deltaTime; // ������ �ϰ�
            if (currentHeight < finalHeight)
                currentHeight = finalHeight; // ���� ���� ����
        }

        // ȸ���� ��ǥ ������ �ʰ������� ����
        if (currentAngle >= upTargetAngle)
        {
            camera.transform.LookAt(camera.target); // ���� ���� ĳ���͸� ����
            return; // ȸ�� ����
        }

        angleStep = upRotationSpeed * Time.deltaTime;

        // ī�޶� target�� �������� ȸ��
        camera.transform.RotateAround(
            camera.target.position,        // ȸ�� �߽� (ĳ������ ��ġ)
            Vector3.up,             // ȸ�� �� (Y�� �������� ȸ��)
            angleStep
        );

        // ���� ���� ����
        currentAngle += angleStep;


        // ȸ�� ��, ī�޶� ���̸� ���� ���̿� �°� ����
        Vector3 currentPosition = camera.transform.position;
        currentPosition.y = currentHeight; // ���� ����
        camera.transform.position = currentPosition;

        // �׻� ĳ���͸� �ٶ󺸰� ����
        camera.transform.LookAt(camera.target);
    }
}
