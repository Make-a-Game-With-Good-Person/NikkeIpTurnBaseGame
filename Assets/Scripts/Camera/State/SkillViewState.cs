using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class SkillViewState : ICameraState
{
    float upRotationSpeed = 120f; // ī�޶� ȸ�� �ӵ�
    float downRotationSpeed = 240f;
    float descendSpeed = 4f; // ī�޶� �ϰ� �ӵ�
    float finalHeight = 1f; // ���� ���� �� ī�޶��� ����

    float upTargetAngle = 280f; // ī�޶� ���� ��ǥ ���� (�÷��̾� ���� ����)
    float downTargetAngle = 520f;
    
    private float currentAngle = 0f; // ���� ȸ�� ���� ����
    float angleStep;

    public void UpdateState(CameraStateController camera)
    {
        // ���� ���̰� ��ǥ ���̺��� ũ�� ��� �ϰ�
        if (camera.currentHeight > finalHeight)
        {
            camera.currentHeight -= descendSpeed * Time.deltaTime; // ������ �ϰ�
            if (camera.currentHeight < finalHeight)
                camera.currentHeight = finalHeight; // ���� ���� ����
        }

        

        if (camera.skillCamDir == 1) // ���� ����
        {
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
        }
        else if (camera.skillCamDir == 2)
        {
            // ȸ���� ��ǥ ������ �ʰ������� ����
            if (currentAngle >= downTargetAngle)
            {
                camera.transform.LookAt(camera.target); // ���� ���� ĳ���͸� ����
                return; // ȸ�� ����
            }

            angleStep = downRotationSpeed * Time.deltaTime;

            // ī�޶� target�� �������� ȸ��
            camera.transform.RotateAround(
                camera.target.position,        // ȸ�� �߽� (ĳ������ ��ġ)
                Vector3.down,             // ȸ�� �� (Y�� �������� ȸ��)
                angleStep
            );
        }

        // ���� ���� ����
        currentAngle += angleStep;


        // ȸ�� ��, ī�޶� ���̸� ���� ���̿� �°� ����
        Vector3 currentPosition = camera.transform.position;
        currentPosition.y = camera.currentHeight; // ���� ����
        camera.transform.position = currentPosition;

        // �׻� ĳ���͸� �ٶ󺸰� ����
        camera.transform.LookAt(camera.target);
    }
}
